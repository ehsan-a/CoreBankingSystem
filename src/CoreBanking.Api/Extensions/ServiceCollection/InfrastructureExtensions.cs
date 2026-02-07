using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Constants;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Audit;
using CoreBanking.Infrastructure.ExternalServices.CentralBankCreditCheck;
using CoreBanking.Infrastructure.ExternalServices.CivilRegistry;
using CoreBanking.Infrastructure.ExternalServices.PoliceClearance;
using CoreBanking.Infrastructure.Generators;
using CoreBanking.Infrastructure.Idempotency;
using CoreBanking.Infrastructure.Identity;
using CoreBanking.Infrastructure.Logging;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Repositories;
using CoreBanking.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<CoreBankingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CoreBankingContext")
                ?? throw new InvalidOperationException("Connection string 'CoreBankingContext' not found.")));

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CoreBankingContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<INumberGenerator, AccountNumberGenerator>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            var configSection = configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
            services.Configure<BaseUrlConfiguration>(configSection);
            var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();


            services.AddHttpClient<ICivilRegistryService, CivilRegistryClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.CivilRegistryBaseAddress));

            services.AddHttpClient<ICentralBankCreditCheckService, CentralBankCreditCheckClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.CentralBankCreditCheckBaseAddress));

            services.AddHttpClient<IPoliceClearanceService, PoliceClearanceClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.PoliceClearanceBaseAddress));

            return services;
        }
    }
}
