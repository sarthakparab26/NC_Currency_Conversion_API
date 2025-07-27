using Microsoft.EntityFrameworkCore;
using NC_Currency_Conversion_API.Repository.IRepository;
using NC_Currency_Conversion_API.Models;
using NC_Currency_Conversion_API.Repository;
using NC_Currency_Conversion_API.Services;
using NC_Currency_Conversion_API.AppDbContext;
using NC_Currency_Conversion_API.Jobs;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(@"Data Source=C:\sqlite\currency_converter.db")); 
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddHostedService<CurrencyRateJob>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
