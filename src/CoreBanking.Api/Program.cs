using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Services;
using CoreBanking.Infrastructure.ExternalServices.CentralBankCreditCheck;
using CoreBanking.Infrastructure.ExternalServices.CivilRegistry;
using CoreBanking.Infrastructure.ExternalServices.PoliceClearance;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Repositories;
using CoreBanking.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CoreBankingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreBankingContext") ?? throw new InvalidOperationException("Connection string 'CoreBankingContext' not found.")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<ICivilRegistryService, CivilRegistryClient>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7293/");
});

builder.Services.AddHttpClient<ICentralBankCreditCheckService, CentralBankCreditCheckClient>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7272/");
});

builder.Services.AddHttpClient<IPoliceClearanceService, PoliceClearanceClient>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7066/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
