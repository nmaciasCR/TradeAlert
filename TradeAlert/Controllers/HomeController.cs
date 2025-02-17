using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;
using TradeAlert.Data.Entities;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStocks _businessStocks;
        private readonly IMarkets _businessMarkets;
        private readonly IPortfolio _businessPortfolio;
        private readonly ICurrency _businessCurrency;


        public HomeController(IStocks businessStocks, IMarkets businessMarkets, IPortfolio businessPortfolio, ICurrency businessCurrency)
        {
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets;
            _businessPortfolio = businessPortfolio;
            _businessCurrency = businessCurrency;
        }


        /// <summary>
        /// Listado de stocks para la grilla de bloques de la pagina principal
        /// </summary>
        [HttpGet]
        [Route("GetStocksOrderAlerts")]
        public async Task<IActionResult> GetStocksOrderAlerts(int priorityId)
        {
            List<Data.DTO.StocksDTO> quotesDTO;

            try
            {
                quotesDTO = await _businessStocks.GetListByPriority(priorityId);

                return StatusCode(StatusCodes.Status200OK, quotesDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        /// <summary>
        /// Retorna listado de N stocks ordenados por ChangePercent (ASC O DESC)
        /// </summary>
        /// <param name="take">Cantidad de items retornados</param>
        /// <param name="order">ASC o DESC</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStocksOrderByChangePercent")]
        public async Task<IActionResult> GetStocksOrderByChangePercent(int take, string order)
        {
            List<Data.Entities.Quotes> quotes;
            List<StocksDTO> quotesDTO = new List<StocksDTO>();

            try
            {

                if (order.ToUpper() == "ASC")
                {
                    quotes = _businessStocks.GetList()
                        .OrderBy(q => q.regularMarketChangePercent)
                        .Take(take)
                        .ToList();
                }
                else if (order.ToUpper() == "DESC")
                {
                    quotes = _businessStocks.GetList()
                         .OrderByDescending(q => q.regularMarketChangePercent)
                         .Take(take)
                         .ToList();
                }
                else
                {
                    quotes = _businessStocks.GetList()
                        .OrderBy(q => q.regularMarketChangePercent)
                        .Take(take)
                        .ToList();
                }


                quotes.ForEach(q => {
                    StocksDTO newStock = _businessStocks.MapToDTO(q);
                    newStock._market = _businessMarkets.MapToDTO(q.market);
                    //Si es parte de portfolio lo mapeamos
                    if (q.Portfolio != null)
                    {
                        newStock._Portfolio = _businessPortfolio.MapToDTO(q.Portfolio);
                    }
                    quotesDTO.Add(newStock);
                });


                return StatusCode(StatusCodes.Status200OK, quotesDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
