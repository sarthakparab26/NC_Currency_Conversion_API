using Microsoft.AspNetCore.Mvc;
using NC_Currency_Conversion_API.Repository.IRepository;
using NC_Currency_Conversion_API.Models;

namespace NC_Currency_Conversion_API.Controllers{
[ApiController]
[Route("api/[controller]")]
public class CurrencyController  : ControllerBase
{
   private readonly ICurrencyRateService _currencyService;
    

    public CurrencyController (ICurrencyRateService currencyService)
    {
        _currencyService=currencyService;
    }

    [HttpGet("rates")]
    public async Task<IActionResult> GetRates()
    {
        var result = await _currencyService.GetLatestRatesAsync();
        return Ok(result);
    }
    [HttpPost("convert")]
    public async Task<IActionResult> Convert([FromBody] ConversionRequestDto request)
    {
        var converted = await _currencyService.ConvertToDKKAsync(request.FromCurrency, request.Amount);
        if (converted == null)
            return NotFound("Currency not found.");

        return Ok(new ConversionResponseDto
        {
            FromCurrency = request.FromCurrency,
            OriginalAmount = request.Amount,
            ConvertedAmount = converted.Value
        });
    }
}
}