using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationManager.Business.Interfaces;
using NotificationManager.Business;
using TradeAlert.MemoryCache;
using TradeAlert.MemoryCache.Interfaces;
using System;

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
            services.AddSingleton<IProcessAlerts, ProcessAlerts>();
            services.AddSingleton<IProcessNotifications, ProcessNotifications>();
            //Calendar Manager
            services.AddHostedService<CalendarManager.Worker>();
            services.AddSingleton<CalendarManager.Business.ICalendar, CalendarManager.Business.Calendar>();

            //Base de datos
            services.AddDbContext<TradeAlert.Data.Entities.TradeAlertContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TradeAlert"));
                //.UseLazyLoadingProxies();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<Business.Interfaces.IStocks, Business.Stocks>();
            services.AddTransient<Business.Interfaces.IQuotesAlerts, Business.QuotesAlerts>();
            services.AddTransient<Business.Interfaces.IMarkets, Business.Markets>();
            services.AddTransient<Business.Interfaces.IPortfolio, Business.Portfolio>();
            services.AddTransient<Business.Interfaces.ICurrency, Business.Currency>();
            services.AddTransient<Business.Interfaces.INotifications, Business.Notifications>();
            services.AddTransient<Business.Interfaces.ICalendar, Business.Calendar>();
            services.AddTransient<Business.Interfaces.IGroups, Business.Groups>();

            //Memory Cache
            services.AddMemoryCache(); // Habilita IMemoryCache
            services.AddTransient<MemoryCache.Interfaces.IStocks, MemoryCache.Business.Stocks>();
            services.AddTransient<MemoryCache.Interfaces.IMarkets, MemoryCache.Business.Market>();
            services.AddTransient<MemoryCache.Interfaces.IPortfolio, MemoryCache.Business.Portfolio>();
            services.AddTransient<MemoryCache.Interfaces.ICurrencies, MemoryCache.Business.Currencies>();
            services.AddTransient<MemoryCache.Interfaces.IQuotesAlerts, MemoryCache.Business.QuotesAlerts>();

            services.AddScoped<IMemoryCacheService, MemoryCacheService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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


            ////Generamos la cache de stocks inicialmente
            //var cacheService = serviceProvider.GetRequiredService<IMemoryCacheService>();
            //cacheService.RefreshStocksAllCache();


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
