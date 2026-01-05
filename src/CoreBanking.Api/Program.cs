using CoreBanking.Api.Extensions.ApplicationBuilder;
using CoreBanking.Api.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddCustomMediatR()
    .AddValidation()
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddCustomCors(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddSwaggerDocumentation()
    .AddRateLimiting(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCustomMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("DevCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.Run();
