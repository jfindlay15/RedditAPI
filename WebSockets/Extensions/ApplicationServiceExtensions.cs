using WebSockets.Helpers;
using WebSockets.Interfaces;
using WebSockets.Services;

namespace WebSockets.Extensions
{    
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RedditSettings>(config.GetSection("RedditSettings"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRedditService, RedditService>();
            services.AddScoped<IMessageProcessor, MessageProcessor>();
            services.AddSingleton<IMetricTracking, MetricTracking>();            
            return services;
        }
    }
}
