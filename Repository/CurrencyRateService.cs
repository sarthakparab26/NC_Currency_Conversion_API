using Microsoft.EntityFrameworkCore;
using NC_Currency_Conversion_API.Repository.IRepository;
using NC_Currency_Conversion_API.Models;
using NC_Currency_Conversion_API.AppDbContext;
namespace NC_Currency_Conversion_API.Repository
{
public class CurrencyRateService : ICurrencyRateService
{
    private readonly ApplicationDbContext _context;

    public CurrencyRateService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CurrencyConversion>> GetLatestRatesAsync()
    {
       return await _context.CurrencyRates.ToListAsync();
    }
    public async Task<ExchangeRates?> GetRateByCurrencyCodeAsync(string code)
    {
        return await _context.ExchangeRates.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task UpdateRatesAsync(IEnumerable<CurrencyConversion> rates)
    {
        _context.CurrencyRates.RemoveRange(_context.CurrencyRates);
        await _context.CurrencyRates.AddRangeAsync(rates);
        await _context.SaveChangesAsync();
    }

    public async Task StoreConversionAsync(CurrencyConversion conversion)
    {
        await _context.CurrencyConversions.AddAsync(conversion);
        await _context.SaveChangesAsync();
    }
    public async Task<decimal?> ConvertToDKKAsync(string fromCurrency, decimal amount)
    {
        var value = await GetRateByCurrencyCodeAsync(fromCurrency);
        if (value == null) return null;

        var convertedAmount = amount * value.Rate;
        await StoreConversionAsync(new CurrencyConversion
        {
            FromCurrency = fromCurrency,
            Amount = amount,
            ConvertedAmount = convertedAmount,
            ConvertedAt = DateTime.UtcNow
        });

        return convertedAmount;
    }
}
}