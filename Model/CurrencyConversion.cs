namespace NC_Currency_Conversion_API.Models{
public class CurrencyConversion
{
    public int Id { get; set; }
    public string FromCurrency { get; set; }
    public decimal Amount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public DateTime ConvertedAt { get; set; }
}
}
