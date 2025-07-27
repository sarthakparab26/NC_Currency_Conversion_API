namespace NC_Currency_Conversion_API.Models{
    public class ExchangeRates
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
}
}