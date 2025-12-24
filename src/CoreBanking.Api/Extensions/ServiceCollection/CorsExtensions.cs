namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevCors", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
