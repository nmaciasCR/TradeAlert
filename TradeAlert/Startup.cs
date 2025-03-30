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
using TradeAlert.Interfaces;
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

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<IStocks, Business.Stocks>();
            services.AddScoped<IQuotesAlerts, Business.QuotesAlerts>();
            services.AddScoped<IMarkets, Business.Markets>();
            services.AddScoped<IPortfolio, Business.Portfolio>();
            services.AddScoped<ICurrency, Business.Currency>();
            services.AddScoped<INotifications, Business.Notifications>();
            services.AddScoped<ICalendar, Business.Calendar>();
            services.AddScoped<IGroups, Business.Groups>();

            //Registrar Lazy<T> manualmente
            services.AddScoped(provider => new Lazy<IStocks>(() => provider.GetRequiredService<IStocks>()));

            //Memory Cache
            services.AddMemoryCache(); // Habilita IMemoryCache
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
