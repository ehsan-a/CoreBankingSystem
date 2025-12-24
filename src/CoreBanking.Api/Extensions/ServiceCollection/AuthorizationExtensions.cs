namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Accessibility", policy =>
                    policy.RequireClaim("Permission", "Full"));
            });

            return services;
        }
    }
}
