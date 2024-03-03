using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolio _businessPortfolio;
        private readonly IStocks _businessStocks;
        private readonly IMarkets _businessMarkets;
        private readonly ICurrency _businessCurrency;

        public PortfolioController(IPortfolio businessPortfolio, IStocks businessStocks, IMarkets businessMarkets, ICurrency businessCurrency)
        {
            _businessPortfolio = businessPortfolio;
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets;
            _businessCurrency = businessCurrency;
        }


        [HttpGet]
        [Route("GetPortfolio")]
        public IActionResult GetPortfolio()
        {
            List<PortfolioDTO> listDTO = new List<PortfolioDTO>();

            try
            {
                List<Data.Entities.Portfolio> portfolioList = _businessPortfolio.GetList();
                portfolioList.ForEach(portfolio =>
                {
                    PortfolioDTO newPortfolio = _businessPortfolio.MapToDTO(portfolio);
                    newPortfolio.weightingPercent = _businessPortfolio.GetWeightingPercent(portfolioList.Sum(p => p.euroTotalAmount), portfolio.euroTotalAmount);
                    newPortfolio._quote = _businessStocks.MapToDTO(portfolio.quote);
                    newPortfolio._quote._market = _businessMarkets.MapToDTO(portfolio.quote.market);
                    newPortfolio._quote._currency = _businessCurrency.MapToDTO(portfolio.quote.currency);
                    newPortfolio.averagePurchasePrice = portfolio.averagePurchasePrice;
                    newPortfolio.euroProfit = portfolio.profit * portfolio.quote.currency.euroExchange;
                    newPortfolio.profitPercent = portfolio.profitPercent;
                    listDTO.Add(newPortfolio);
                });

                return StatusCode(StatusCodes.Status200OK, listDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Actualiza una accion del portfolio
        /// </summary>
        [HttpPost]
        [Route("UpdatePortfolioStock")]
        public IActionResult UpdatePortfolioStock(Business.Request.UpdatePortfolio pStock)
        {
            try
            {
                if (_businessPortfolio.Update(pStock))
                {
                    return StatusCode(StatusCodes.Status200OK);
                } else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "ERROR 400: ERROR AL ACTUALIZAR UNA ACCION DEL PORTFOLIO");
                }

            } catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL ACTUALIZAR UNA ACCION DEL PORTFOLIO");
            }

        }

        /// <summary>
        /// Elimina una accion del portfolio
        /// </summary>
        [HttpDelete]
        [Route("DeleteStockPortfolio")]
        public IActionResult DeleteStockPortfolio(int idPortfolio)
        {

            try
            {
                if (_businessPortfolio.Delete(idPortfolio))
                {
                    return StatusCode(StatusCodes.Status200OK);
                } else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "ERROR 400: ERROR AL ELIMINAR UNA ACCION DEL PORTFOLIO");
                }


            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL ELIMINAR UNA ACCION DEL PORTFOLIO");
            }



        }

    }
}
