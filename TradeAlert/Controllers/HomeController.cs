using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStocks _businessStocks;


        public HomeController(IStocks businessStocks)
        {
            _businessStocks = businessStocks;
        }

        [HttpGet]
        [Route("GetStocksOrderAlerts")]
        public IActionResult GetStocksOrderAlerts(int priorityId)
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _businessStocks.GetListByPriority(priorityId));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

    }
}
