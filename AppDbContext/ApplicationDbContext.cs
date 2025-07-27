
using Microsoft.EntityFrameworkCore;
using NC_Currency_Conversion_API.Models;    
namespace NC_Currency_Conversion_API.AppDbContext
{    
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<CurrencyConversion> CurrencyRates { get; set; }
    public DbSet<ExchangeRates> ExchangeRates { get; set; }
     public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

}
}