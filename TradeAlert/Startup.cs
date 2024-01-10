using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationManager.Business.Interfaces;
using NotificationManager.Business;

namespace TradeAlert
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            //Notification Manager
            services.AddHostedService<NotificationManager.Worker>();
            services.AddTransient<IProcessAlerts, ProcessAlerts>();

            //Base de datos
            services.AddDbContext<TradeAlert.Data.Entities.TradeAlertContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TradeAlert"));
                //.UseLazyLoadingProxies();
            });

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddTransient<Business.Interfaces.IStocks, Business.Stocks>();
            services.AddTransient<Business.Interfaces.IQuotesAlerts, Business.QuotesAlerts>();
            services.AddTransient<Business.Interfaces.IMarkets, Business.Markets>();
            services.AddTransient<Business.Interfaces.IPortfolio, Business.Portfolio>();
            services.AddTransient<Business.Interfaces.ICurrency, Business.Currency>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
