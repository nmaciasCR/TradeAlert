using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Business;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;
using TradeAlert.Business.Request;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocks _businessStocks;
        private readonly IQuotesAlerts _businessQuotesAlerts;
        private readonly IMarkets _businessMarkets;
        private readonly IPortfolio _businessPortfolio;

        public StocksController(IStocks businessStocks, IQuotesAlerts businessQuotesAlerts, IMarkets businessMarkets, IPortfolio businessPortfolio)
        {
            _businessStocks = businessStocks;
            _businessQuotesAlerts = businessQuotesAlerts;
            _businessMarkets = businessMarkets;
            _businessPortfolio = businessPortfolio;
        }

        /// <summary>
        /// Listado de acciones para mostrar en tabla
        /// </summary>
        [HttpGet]
        [Route("GetStocks")]
        public IActionResult GetStocks()
        {
            try
            {
                List<StocksDTO> quotesDTOList = new();

                foreach (Data.Entities.Quotes q in _businessStocks.GetList())
                {
                    StocksDTO stocksDTO = _businessStocks.MapToDTO(q);
                    stocksDTO._market = _businessMarkets.MapToDTO(q.market);
                    stocksDTO._Portfolio = _businessPortfolio.MapToDTO(q.Portfolio);
                    quotesDTOList.Add(stocksDTO);
                }

                return StatusCode(StatusCodes.Status200OK, quotesDTOList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet]
        [Route("GetQuotesAlerts")]
        public IActionResult GetQuotesAlerts(int stockId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _businessQuotesAlerts.GetList(stockId));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddQuoteAlert")]
        public IActionResult AddQuoteAlert(AddQuoteAlert newAlert)
        {
            try
            {
                _businessStocks.AddAlert(newAlert.QuoteId, newAlert.TypeId, newAlert.Price);
                return StatusCode(StatusCodes.Status200OK, newAlert);


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("DeleteQuoteAlert")]
        public IActionResult DeleteQuoteAlert(DeleteQuoteAlert alertToDelete)
        {
            try
            {
                _businessStocks.DeleteAlert(alertToDelete.QuoteId, alertToDelete.AlertId);

                return StatusCode(StatusCodes.Status200OK);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetMainStocks")]
        public IActionResult GetMainStocks()
        {

            try
            {

                List<StocksDTO> mainStockDTO = _businessStocks.GetList()
                    .Where(s => s.isMainIndex)
                    .Select(x => _businessStocks.MapToDTO(x))
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, mainStockDTO);

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
