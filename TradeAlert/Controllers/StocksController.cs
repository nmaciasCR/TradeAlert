using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Business;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly Business.Interfaces.IStocks _businessStocks;
        private readonly IQuotesAlerts _businessQuotesAlerts;

        public StocksController(Business.Interfaces.IStocks businessStocks, Business.Interfaces.IQuotesAlerts businessQuotesAlerts)
        {
            _businessStocks = businessStocks;
            _businessQuotesAlerts = businessQuotesAlerts;
        }


        [HttpGet]
        [Route("GetStocks")]
        public IActionResult GetStocks()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _businessStocks.GetList());
                

            } catch (Exception ex)
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


            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddQuoteAlert")]
        public IActionResult AddQuoteAlert(Business.Model.Request.AddQuoteAlert newAlert)
        {
            try
            {
                _businessStocks.AddAlert(newAlert.QuoteId, newAlert.TypeId, newAlert.Price);
                return StatusCode(StatusCodes.Status200OK, newAlert);


            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("DeleteQuoteAlert")]
        public IActionResult DeleteQuoteAlert(Business.Model.Request.DeleteQuoteAlert alertToDelete)
        {
            try
            {
                _businessStocks.DeleteAlert(alertToDelete.QuoteId, alertToDelete.AlertId);

                return StatusCode(StatusCodes.Status200OK);

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
