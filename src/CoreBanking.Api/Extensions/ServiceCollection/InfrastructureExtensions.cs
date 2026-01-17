using CoreBanking.Application.EventHandlers.Accounts;
using CoreBanking.Application.EventHandlers.Authentications;
using CoreBanking.Application.EventHandlers.Customers;
using CoreBanking.Application.EventHandlers.Transactions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Constants;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Events.Accounts;
using CoreBanking.Domain.Events.Authentications;
using CoreBanking.Domain.Events.Customers;
using CoreBanking.Domain.Events.Transactions;
using CoreBanking.Infrastructure.Audit;
using CoreBanking.Infrastructure.Events;
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
using CoreBanking.Infrastructure.UnitOfWork;
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INumberGenerator, AccountNumberGenerator>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IDomainEventHandler<TransactionCreatedEvent>, TransactionCreatedAuditHandler>();
            services.AddScoped<IDomainEventHandler<TransactionCreatedEvent>, TransactionCreatedIdempotencyHandler>();

            services.AddScoped<IDomainEventHandler<CustomerCreatedEvent>, CustomerCreatedAuditHandler>();
            services.AddScoped<IDomainEventHandler<CustomerDeletedEvent>, CustomerDeletedAuditHandler>();
            services.AddScoped<IDomainEventHandler<CustomerUpdatedEvent>, CustomerUpdatedAuditHandler>();

            services.AddScoped<IDomainEventHandler<AccountCreatedEvent>, AccountCreatedAuditHandler>();
            services.AddScoped<IDomainEventHandler<AccountDeletedEvent>, AccountDeletedAuditHandler>();
            services.AddScoped<IDomainEventHandler<AccountUpdatedEvent>, AccountUpdatedAuditHandler>();

            services.AddScoped<IDomainEventHandler<AuthenticationCreatedEvent>, AuthenticationCreatedAuditHandler>();


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
