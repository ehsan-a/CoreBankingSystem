using CoreBanking.Api.Extensions.ApplicationBuilder;
using CoreBanking.Api.Extensions.ServiceCollection;
using CoreBanking.Api.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddCustomMediatR()
    .AddValidation()
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddCustomCors(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddSwaggerDocumentation()
    .AddRateLimiting(builder.Configuration)
    .AddCustomHealthChecks(builder.Configuration);


var app = builder.Build();

app.UseCustomMiddlewares();

//app.UseHealthChecks("/health",
//    new HealthCheckOptions
//    {
//        ResponseWriter = HealthCheckResponses.WriteResponse
//    });

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

app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = HealthCheckResponses.WriteResponse
    });
//.RequireAuthorization()
//.RequireRateLimiting(RateLimitPolicies.Fixed);

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
    ResponseWriter = HealthCheckResponses.WriteResponse
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false,
});

app.Logger.LogInformation("LAUNCHING CoreBanking.Api");

app.Run();
