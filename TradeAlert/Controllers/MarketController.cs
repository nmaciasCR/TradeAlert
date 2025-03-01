using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Data.DTO;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly Business.Interfaces.IMarkets _businessMarkets;


        public MarketController(Business.Interfaces.IMarkets businessMarkets)
        {

            _businessMarkets = businessMarkets;

        }

        /// <summary>
        /// Retorna todos los mercados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList() {
        

            try
            {

                List<MarketDTO> markets = _businessMarkets.GetList()
                                            .ToList()
                                            .Select(m =>
                                            {
                                                MarketDTO market = _businessMarkets.MapToDTO(m);
                                                market.QuotesQty = m.Quotes.Count();
                                                return market;
                                            })
                                            .OrderBy(m => m.description)
                                            .ToList();


                return StatusCode(StatusCodes.Status200OK, markets);

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }

    }
}
