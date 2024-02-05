using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TradeAlert.Business;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;
using TradeAlert.Controllers;
using TradeAlert.Data.Entities;

namespace TradeAlert.Testing
{
    public class Endpoints_Home
    {

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
        [InlineData(-3)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(8)]
        public void Should_GetStocksOrderAlerts_Return200(int priority)
        {
            //Arrange
            Mock<IStocks> businessStockMock = new Mock<IStocks>();
            Mock<IMarkets> businessMarketMock = new Mock<IMarkets>();
            Mock<IPortfolio> businessPortfolioMock = new Mock<IPortfolio>();
            Mock<ICurrency> businessCurrencyMock = new Mock<ICurrency>();
            HomeController _homeController = new HomeController(businessStockMock.Object, businessMarketMock.Object, businessPortfolioMock.Object, businessCurrencyMock.Object);

            businessStockMock.Setup(bs => bs.GetListByPriority(priority))
                             .Returns(new List<Quotes>());
            businessStockMock.Setup(bs => bs.MapToDTO(new Data.Entities.Quotes()))
                             .Returns(new StocksDTO());
            businessMarketMock.Setup(bm => bm.MapToDTO(new Data.Entities.Markets()))
                              .Returns(new MarketDTO());
            businessPortfolioMock.Setup(bp => bp.MapToDTO(new Data.Entities.Portfolio()))
                              .Returns(new PortfolioDTO());
            businessCurrencyMock.Setup(bc => bc.MapToDTO(new Data.Entities.Currencies()))
                                .Returns(new CurrencyDTO());

            //Act
            var response = _homeController.GetStocksOrderAlerts(priority);
            var objectResult = Assert.IsType<ObjectResult>(response);

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Theory]
        [InlineData(1, "ASC")]
        [InlineData(2, "DESC")]
        [InlineData(1, "ABC")]
        public void Should_GetStocksOrderByChangePercent_Return200(int take, string order)
        {
            //Arrange
            Mock<IStocks> businessStockMock = new Mock<IStocks>();
            Mock<IMarkets> businessMarketMock = new Mock<IMarkets>();
            Mock<IPortfolio> businessPortfolioMock = new Mock<IPortfolio>();
            Mock<ICurrency> businessCurrencyMock = new Mock<ICurrency>();
            HomeController _homeController = new HomeController(businessStockMock.Object, businessMarketMock.Object, businessPortfolioMock.Object, businessCurrencyMock.Object);

            businessStockMock.Setup(bs => bs.GetList())
                 .Returns(new List<Quotes>
                 {
                     new Quotes
                     {
                         ID = 1,
                         currencyId = 1,
                         name = "PBR",
                         regularMarketChangePercent = (Decimal)1.65,
                         market = new Data.Entities.Markets()
                         {
                             ID = 2,
                             description = ""
                         }
                     }
                 });
            businessStockMock.Setup(bs => bs.MapToDTO(It.IsAny<Data.Entities.Quotes>()))
                             .Returns(new StocksDTO());
            businessMarketMock.Setup(bm => bm.MapToDTO(It.IsAny<Data.Entities.Markets>()))
                              .Returns(new MarketDTO())
                              .Callback(() => Console.WriteLine("MapToDTO called with any parameter."));
            businessPortfolioMock.Setup(bp => bp.MapToDTO(new Data.Entities.Portfolio()))
                                 .Returns(new PortfolioDTO());

            //Act
            var response = _homeController.GetStocksOrderByChangePercent(take, order);
            var objectResult = Assert.IsType<ObjectResult>(response);

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}