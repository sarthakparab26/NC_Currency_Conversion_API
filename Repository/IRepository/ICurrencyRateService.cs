using NC_Currency_Conversion_API.Models;
namespace NC_Currency_Conversion_API.Repository.IRepository
{
public interface ICurrencyRateService
{
    Task<List<CurrencyConversion>> GetLatestRatesAsync();
     Task<ExchangeRates?> GetRateByCurrencyCodeAsync(string code);
    Task UpdateRatesAsync(IEnumerable<CurrencyConversion> rates);
    Task StoreConversionAsync(CurrencyConversion conversion);
     Task<decimal?> ConvertToDKKAsync(string fromCurrency, decimal amount);
}

}
