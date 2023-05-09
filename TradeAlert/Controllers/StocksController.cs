using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Business;
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

        public StocksController(IStocks businessStocks, IQuotesAlerts businessQuotesAlerts)
        {
            _businessStocks = businessStocks;
            _businessQuotesAlerts = businessQuotesAlerts;
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

                return StatusCode(StatusCodes.Status200OK, _businessStocks.GetList().Select(s => _businessStocks.MapToDTO(s)));
                

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
        public IActionResult AddQuoteAlert(AddQuoteAlert newAlert)
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
        public IActionResult DeleteQuoteAlert(DeleteQuoteAlert alertToDelete)
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
