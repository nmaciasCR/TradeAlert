using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.Interfaces;
using TradeAlert.Data.Request;


namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocks _businessStocks;
        private readonly IQuotesAlerts _businessQuotesAlerts;
        private readonly IMarkets _businessMarkets;
        private readonly IPortfolio _businessPortfolio;
        private readonly ICurrency _businessCurrency;
        private readonly IGroups _businessGroups;

        public StocksController(IStocks businessStocks, IQuotesAlerts businessQuotesAlerts, IMarkets businessMarkets, IPortfolio businessPortfolio,
            ICurrency businessCurrency, IGroups businessGroups)
        {
            _businessStocks = businessStocks;
            _businessQuotesAlerts = businessQuotesAlerts;
            _businessMarkets = businessMarkets;
            _businessPortfolio = businessPortfolio;
            _businessCurrency = businessCurrency;
            _businessGroups = businessGroups;   
        }

        /// <summary>
        /// Listado de acciones para mostrar en tabla
        /// </summary>
        [HttpGet]
        [Route("GetStocks")]
        public async Task<IActionResult> GetStocks()
        {
            try
            {
                List<Data.DTO.StocksDTO> quotesDTOList = await _businessStocks.GetListAsync();

                return StatusCode(StatusCodes.Status200OK, quotesDTOList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }


        /// <summary>
        /// Retorna las alertas de una accion
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuotesAlerts")]
        public IActionResult GetQuotesAlerts(int stockId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _businessQuotesAlerts.GetList(stockId));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Agregamos una alerta a una accion
        /// </summary>
        [HttpPost]
        [Route("AddQuoteAlert")]
        public IActionResult AddQuoteAlert(AddQuoteAlert newAlert)
        {
            try
            {
                _businessStocks.AddAlert(newAlert.QuoteId, newAlert.TypeId, newAlert.Price);
                return StatusCode(StatusCodes.Status200OK, newAlert);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Eliminamos una alerta de una accion
        /// </summary>
        [HttpPost]
        [Route("DeleteQuoteAlert")]
        public IActionResult DeleteQuoteAlert(DeleteQuoteAlert alertToDelete)
        {
            try
            {
                _businessStocks.DeleteAlert(alertToDelete.QuoteId, alertToDelete.AlertId);

                return StatusCode(StatusCodes.Status200OK);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retorna los indices principales (los que van en el header)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMainStocks")]
        public async Task<IActionResult> GetMainStocks()
        {

            try
            {
                List<StocksDTO> mainStockDTO = (await _businessStocks.GetListAsync())
                                                        .Where(s => s.isMainIndex)
                                                        .ToList();

                return StatusCode(StatusCodes.Status200OK, mainStockDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Retornamos todas las acciones mapeadas para generar un autocomplete
        /// </summary>
        [HttpGet]
        [Route("GetStockAutocomplete")]
        public IActionResult GetStockAutocomplete()
        {
            try
            {
                List<StockAutocompleteDTO> stockAutocomplete = _businessStocks.GetList()
                    .Select(s => new StockAutocompleteDTO
                    {
                        id = s.ID,
                        symbol = s.symbol,
                        name = s.name,
                        displayName = $"{s.symbol} - {s.name}"
                    })
                    .ToList()
                    .OrderBy(x => x.displayName)
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, stockAutocomplete);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
