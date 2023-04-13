using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}
