using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using ProxyApp.Services;
using System.Threading.RateLimiting;

namespace ProxyApp
{
    public class Startup
    { 
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRateLimiter(ratelimiteroptions =>
            {
                ratelimiteroptions.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 3; 
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 3;
                });
            }); 
            services.AddScoped<IExternalApiService, ExternalApiService>();
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseRateLimiter(); 
            app.UseStaticFiles();
            app.UseRouting(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      "default",
                        "{controller=Home}/{action=Index}/{id?}"
                    );
                 
            });
        }
    }
}
