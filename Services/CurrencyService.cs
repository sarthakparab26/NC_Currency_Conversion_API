using NC_Currency_Conversion_API.Repository;
using NC_Currency_Conversion_API.Repository.IRepository;
using NC_Currency_Conversion_API.Models;
namespace NC_Currency_Conversion_API.Services{
public class CurrencyService
{
    private readonly ICurrencyRateService _repository;

    public CurrencyService(ICurrencyRateService repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CurrencyConversion>> GetRatesAsync()
        => await _repository.GetLatestRatesAsync();

    
}

}