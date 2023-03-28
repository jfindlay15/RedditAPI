using WebSockets.Extensions;
using WebSockets.Interfaces;

namespace WebSockets
{
    public class Program
    {        
        public static void Main(string[] args)
        {                        
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();            

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var redditService = services.GetRequiredService<IRedditService>();
            _=  redditService.Run();

            app.Run();
        }               
    }
}