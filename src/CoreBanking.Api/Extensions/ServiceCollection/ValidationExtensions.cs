using CoreBanking.Application.CQRS.Behaviors;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("CoreBanking.Application"));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
