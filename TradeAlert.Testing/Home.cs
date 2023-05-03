using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using TradeAlert.Business;
using TradeAlert.Business.Interfaces;
using TradeAlert.Controllers;
using TradeAlert.Data.Entities;

namespace TradeAlert.Testing
{
    public class Home
    {
        private readonly HomeController _homeController;

        public Home()
        {
            IConfigurationRoot _config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();


            var services = new ServiceCollection();
            services.AddDbContext<TradeAlertContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("TradeAlert")));
            services.AddScoped<IStocks, Stocks>();
            var serviceProvider = services.BuildServiceProvider();

            _homeController = new HomeController(serviceProvider.GetService<IStocks>());

        }

        //[Fact]
        //public void Should_GetStocksOrderAlerts_Return200()
        //{
        //    //Arrange
        //    //Act
        //    var response = _homeController.GetStocksOrderAlerts(1);
        //    var objectResult = Assert.IsType<ObjectResult>(response);


        //    //Assert
        //    Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);

        //}

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 200)]
        [InlineData(3, 200)]
        [InlineData(-1, 200)]
        public void Should_GetStocksOrderAlerts_Return200(int priority, int statusCode)
        {
            //Arrange
            //Act
            var response = _homeController.GetStocksOrderAlerts(priority);
            var objectResult = Assert.IsType<ObjectResult>(response);


            //Assert
            Assert.Equal(statusCode, objectResult.StatusCode);

        }

    }
}