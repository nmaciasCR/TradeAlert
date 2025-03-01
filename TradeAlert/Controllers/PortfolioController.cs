using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
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
        private readonly ICurrency _businessCurrency;

        public PortfolioController(IPortfolio businessPortfolio, IStocks businessStocks, IMarkets businessMarkets, ICurrency businessCurrency)
        {
            _businessPortfolio = businessPortfolio;
            _businessStocks = businessStocks;
            _businessMarkets = businessMarkets;
            _businessCurrency = businessCurrency;
        }


        [HttpGet]
        [Route("GetPortfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            List<PortfolioDTO> listDTO = new List<PortfolioDTO>();

            try
            {
                //Listado de acciones del portfolio
                List<StocksDTO> portfolioStocks = await _businessPortfolio.GetListFromCache();
                //Total del portfolio (€)
                double euroPortfolioAmount = portfolioStocks.Select(s => s._Portfolio)
                                                            .Sum(p => p.euroTotalAmount);
                portfolioStocks.ForEach(stock =>
                {
                    stock._Portfolio.weightingPercent = _businessPortfolio.GetWeightingPercent(euroPortfolioAmount, stock._Portfolio.euroTotalAmount);
                });

                return StatusCode(StatusCodes.Status200OK, portfolioStocks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Actualiza una accion del portfolio
        /// </summary>
        [HttpPost]
        [Route("UpdatePortfolioStock")]
        public IActionResult UpdatePortfolioStock(Business.Request.UpdatePortfolio pStock)
        {
            try
            {
                //Actualizamos la accion del portfolio
                ProblemDetails response = _businessPortfolio.Update(pStock);

                //Verificamos la respuesta
                if (response.Status == StatusCodes.Status200OK)
                {
                    return StatusCode(StatusCodes.Status200OK);
                } else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

            } catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL ACTUALIZAR UNA ACCION DEL PORTFOLIO");
            }
        }

        /// <summary>
        /// Elimina una accion del portfolio
        /// </summary>
        [HttpDelete]
        [Route("DeleteStockPortfolio")]
        public IActionResult DeleteStockPortfolio(int idPortfolio)
        {

            try
            {
                if (_businessPortfolio.Delete(idPortfolio))
                {
                    return StatusCode(StatusCodes.Status200OK);
                } else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "ERROR 400: ERROR AL ELIMINAR UNA ACCION DEL PORTFOLIO");
                }


            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL ELIMINAR UNA ACCION DEL PORTFOLIO");
            }
        }

        [HttpPut]
        [Route("AddPortfolioStock")]
        public IActionResult AddPortfolioStock(Business.Request.UpdatePortfolio newPortfolio)
        {
            try
            {
                //Agregamos el nuevo portfolio
                ProblemDetails response = _businessPortfolio.Add(newPortfolio);

                //Validamos la respuesta
                if (response.Status == StatusCodes.Status200OK)
                {
                    //Se pudo agregar correctamente
                    return StatusCode(StatusCodes.Status200OK, response.Extensions["result"]);
                } else
                {
                    //Ocurrio un error al agregar una nueva accion al portfolio
                    return StatusCode(Convert.ToInt32(response.Status), response);
                }

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL INSERTAR UNA ACCION AL PORTFOLIO");
            }
        }

    }
}
