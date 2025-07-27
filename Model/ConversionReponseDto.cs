namespace NC_Currency_Conversion_API.Models
{
public class ConversionResponseDto
{
    public string FromCurrency { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public string ToCurrency => "DKK";
}

}