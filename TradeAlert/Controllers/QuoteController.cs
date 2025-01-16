using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {

        private readonly IStocks _businessStocks;
        private readonly IMarkets _businessMarkets;
        private readonly IPortfolio _businessPortfolio;
        private readonly ICurrency _businessCurrency;
        private readonly IGroups _businessGroups;



        public QuoteController(IStocks businessStocks, IMarkets businessMarkets, IPortfolio businessPortfolio, ICurrency businessCurrency, IGroups businessGroups)
        {
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets;
            _businessPortfolio = businessPortfolio;
            _businessCurrency = businessCurrency;
            _businessGroups = businessGroups;
        }


        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string q)
        {
            try
            {
                Data.Entities.Quotes quote = _businessStocks.GetQuote(q);
                //DTO return
                Business.DTO.StocksDTO quoteDTO = _businessStocks.MapToDTO(quote);
                //Market DTO
                Data.Entities.Markets market = _businessMarkets.Get(quote.marketId);
                quoteDTO._market = _businessMarkets.MapToDTO(market);
                //Currency DTO
                Data.Entities.Currencies currency = _businessCurrency.Get(quote.currencyId);
                quoteDTO._currency = _businessCurrency.MapToDTO(currency);
                //Portfolio
                quoteDTO._Portfolio = quote.Portfolio != null ? _businessPortfolio.MapToDTO(quote.Portfolio) : null;
                //Groups
                quoteDTO._groups = _businessGroups.MapToDTO(quote.QuotesGroups.Select(q => q.Group).ToList());

                return StatusCode(StatusCodes.Status200OK, quoteDTO);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }

    
    }
}
