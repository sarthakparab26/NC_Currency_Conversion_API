namespace NC_Currency_Conversion_API.Models
{
public class ConversionRequestDto
{
    public string FromCurrency { get; set; }
    public decimal Amount { get; set; }
}
}