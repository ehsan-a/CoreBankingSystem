using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Net;
using System.Threading.RateLimiting;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class RateLimitingExtensions
    {

        public static IServiceCollection AddRateLimiting(
    this IServiceCollection services,
    IConfiguration configuration)
        {
            services.Configure<MyRateLimitOptions>(
                configuration.GetSection(MyRateLimitOptions.MyRateLimit));

            services.AddRateLimiter(options =>
            {
                AddRejectionHandling(options);
                AddGlobalLimiter(options, configuration);
                AddConcurrencyLimiter(options, configuration);
                AddTokenBucketRateLimiter(options, configuration);
                AddFixedWindowLimiter(options, configuration);
                AddSlidingWindowRateLimiter(options, configuration);
            });

            return services;
        }

        public static void AddRejectionHandling(RateLimiterOptions options)
        {
            static string GetUserEndPoint(HttpContext context) =>
            $"User {context.User.Identity?.Name ?? "Anonymous"} endpoint:{context.Request.Path}"
            + $" {context.Connection.RemoteIpAddress}";


            options.OnRejected = (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                        ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.RequestServices.GetService<ILoggerFactory>()?
                    .CreateLogger("Microsoft.AspNetCore.RateLimitingMiddleware")
                    .LogWarning("OnRejected: {GetUserEndPoint}", GetUserEndPoint(context.HttpContext));

                return new ValueTask();
            };
        }

        public static void AddGlobalLimiter(RateLimiterOptions options, IConfiguration configuration)
        {
            var myOptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, IPAddress>(context =>
            {
                IPAddress? remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress is not null && !IPAddress.IsLoopback(remoteIpAddress))
                {
                    return RateLimitPartition.GetTokenBucketLimiter
                    (remoteIpAddress!, _ =>
                        new TokenBucketRateLimiterOptions
                        {
                            TokenLimit = myOptions.TokenLimit2,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = myOptions.QueueLimit,
                            ReplenishmentPeriod = TimeSpan.FromSeconds(myOptions.ReplenishmentPeriod),
                            TokensPerPeriod = myOptions.TokensPerPeriod,
                            AutoReplenishment = myOptions.AutoReplenishment
                        });
                }

                return RateLimitPartition.GetNoLimiter(IPAddress.Loopback);
            });
        }

        public static void AddConcurrencyLimiter(RateLimiterOptions options, IConfiguration configuration)
        {
            var myOptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);


            options.AddPolicy(RateLimitPolicies.Concurrency, context =>
       RateLimitPartition.GetConcurrencyLimiter(
           context.User.Identity?.Name ?? "Anon",
           _ => new ConcurrencyLimiterOptions
           {
               PermitLimit = myOptions.PermitLimit,
               QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
               QueueLimit = myOptions.QueueLimit
           }));
        }

        public static void AddTokenBucketRateLimiter(RateLimiterOptions options, IConfiguration configuration)
        {
            var myOptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);


            options.AddPolicy(policyName: RateLimitPolicies.TokenBucket, partitioner: httpContext =>
              {
                  string userName = httpContext.User.Identity?.Name ?? string.Empty;

                  if (!StringValues.IsNullOrEmpty(userName))
                  {
                      return RateLimitPartition.GetTokenBucketLimiter(userName, _ =>
                          new TokenBucketRateLimiterOptions
                          {
                              TokenLimit = myOptions.TokenLimit2,
                              QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                              QueueLimit = myOptions.QueueLimit,
                              ReplenishmentPeriod = TimeSpan.FromSeconds(myOptions.ReplenishmentPeriod),
                              TokensPerPeriod = myOptions.TokensPerPeriod,
                              AutoReplenishment = myOptions.AutoReplenishment
                          });
                  }

                  return RateLimitPartition.GetTokenBucketLimiter("Anon", _ =>
                      new TokenBucketRateLimiterOptions
                      {
                          TokenLimit = myOptions.TokenLimit,
                          QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                          QueueLimit = myOptions.QueueLimit,
                          ReplenishmentPeriod = TimeSpan.FromSeconds(myOptions.ReplenishmentPeriod),
                          TokensPerPeriod = myOptions.TokensPerPeriod,
                          AutoReplenishment = true
                      });
              });
        }

        public static void AddFixedWindowLimiter(RateLimiterOptions options, IConfiguration configuration)
        {
            var myOptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

            options.AddFixedWindowLimiter(policyName: RateLimitPolicies.Fixed, options =>
                {
                    options.PermitLimit = myOptions.PermitLimit;
                    options.Window = TimeSpan.FromSeconds(myOptions.Window);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = myOptions.QueueLimit;
                });
        }

        public static void AddSlidingWindowRateLimiter(RateLimiterOptions options, IConfiguration configuration)
        {
            var myOptions = new MyRateLimitOptions();
            configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

            options.AddPolicy(RateLimitPolicies.Sliding, context =>
            {
                var username = "anonymous user";
                if (context.User.Identity?.IsAuthenticated is true)
                {
                    username = context.User.Identity.Name;
                }

                return RateLimitPartition.GetSlidingWindowLimiter(username,
                    _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = myOptions.PermitLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = myOptions.QueueLimit,
                        Window = TimeSpan.FromSeconds(myOptions.Window),
                        SegmentsPerWindow = myOptions.SegmentsPerWindow
                    });

            });
        }

        public class MyRateLimitOptions
        {
            public const string MyRateLimit = "MyRateLimit";
            public int PermitLimit { get; set; } = 5;
            public int Window { get; set; } = 60;
            public int ReplenishmentPeriod { get; set; } = 2;
            public int QueueLimit { get; set; } = 0;
            public int SegmentsPerWindow { get; set; } = 8;
            public int TokenLimit { get; set; } = 10;
            public int TokenLimit2 { get; set; } = 20;
            public int TokensPerPeriod { get; set; } = 4;
            public bool AutoReplenishment { get; set; } = true;
        }

        public static class RateLimitPolicies
        {
            public const string TokenBucket = "TokenBucket";
            public const string Fixed = "Fixed";
            public const string Sliding = "Sliding";
            public const string Concurrency = "Concurrency";
        }
    }
}
