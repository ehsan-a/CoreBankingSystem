using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Services;
using CoreBanking.Infrastructure.Identity;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            cfg.AddMaps(typeof(CreateAccountCommand).Assembly));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
