using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using NC_Currency_Conversion_API.AppDbContext; // Your DbContext namespace
using NC_Currency_Conversion_API.Models; // Your CurrencyRate model
using System.Globalization;

namespace NC_Currency_Conversion_API.Jobs
{
public class CurrencyRateJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpClientFactory _httpClientFactory;

    public CurrencyRateJob(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
    {
        _serviceProvider = serviceProvider;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateCurrencyRates();

            await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken); // run every 60 minutes
        }
    }

    private async Task UpdateCurrencyRates()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            string xml = await client.GetStringAsync("https://www.nationalbanken.dk/api/currencyratesxml?lang=da");

            XDocument xmlDoc = XDocument.Parse(xml);
            var date = xmlDoc.Root.Element("dailyrates")?.Attribute("id")?.Value;

            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var currencies = xmlDoc.Descendants("currency");

            foreach (var currency in currencies)
            {
                string code = currency.Attribute("code")?.Value;
                string desc = currency.Attribute("desc")?.Value;
                string rateStr = currency.Attribute("rate")?.Value?.Replace(",", ".");

                if (decimal.TryParse(rateStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate))
                {
                    db.ExchangeRates.Add(new ExchangeRates
                    {
                        Date = date,
                        Code = code,
                        Description = desc,
                        Rate = rate
                    });
                }
            }

            await db.SaveChangesAsync();
            Console.WriteLine($"Currency rates updated at {DateTime.Now}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating currency rates: {ex.Message}");
        }
    }
}
}