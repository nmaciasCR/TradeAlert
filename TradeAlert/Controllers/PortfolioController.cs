using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PortfolioController(IPortfolio businessPortfolio, IStocks businessStocks, IMarkets businessMarkets)
        {
            _businessPortfolio = businessPortfolio;
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets; 
        }


        [HttpGet]
        [Route("GetPortfolio")]
        public IActionResult GetPortfolio()
        {
            List<PortfolioDTO> listDTO = new List<PortfolioDTO>();

            try
            {
                _businessPortfolio.GetList().ForEach(portfolio =>
                {
                    PortfolioDTO newPortfolio = _businessPortfolio.MapToDTO(portfolio);
                    newPortfolio._quote = _businessStocks.MapToDTO(portfolio.quote);
                    newPortfolio._quote._market = _businessMarkets.MapToDTO(portfolio.quote.market);
                    listDTO.Add(newPortfolio);
                });

                return StatusCode(StatusCodes.Status200OK, listDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }


    }
}
