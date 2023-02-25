using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly Business.Interfaces.IStocks _businessStocks;


        public StocksController(Business.Interfaces.IStocks businessStocks)
        {
            _businessStocks = businessStocks;
        }


        [HttpGet]
        [Route("GetStocks")]
        public IActionResult GetStocks()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _businessStocks.GetList());
                

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }





    }
}
