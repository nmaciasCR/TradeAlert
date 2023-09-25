using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStocks _businessStocks;
        private readonly IMarkets _businessMarkets;


        public HomeController(IStocks businessStocks, IMarkets businessMarkets)
        {
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets;
        }

        [HttpGet]
        [Route("GetStocksOrderAlerts")]
        public IActionResult GetStocksOrderAlerts(int priorityId)
        {
            try
            {
                List<Data.Entities.Quotes> listByPriority = _businessStocks.GetListByPriority(priorityId);
                return StatusCode(StatusCodes.Status200OK, _businessStocks.MapToDTO(listByPriority));


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
        public IActionResult GetStocksOrderByChangePercent(int take, string order)
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
