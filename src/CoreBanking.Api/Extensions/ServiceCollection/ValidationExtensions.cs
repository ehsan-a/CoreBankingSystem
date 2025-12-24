using CoreBanking.Application.DTOs.Requests.Authentication;
using FluentValidation;
using System.Reflection;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("CoreBanking.Application"));

            return services;
        }
    }
}
