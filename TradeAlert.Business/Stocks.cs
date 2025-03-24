using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.MemoryCache.Interfaces;

namespace TradeAlert.Business
{
    public class Stocks : Interfaces.IStocks
    {

        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheService _memoryCacheService;

        public Stocks(Data.Entities.TradeAlertContext dbContext, IMapper mapper, IMemoryCacheService memoryCacheService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._memoryCacheService = memoryCacheService;
        }


        /// <summary>
        /// Retorna un listado de las acciones
        /// </summary>
        public IQueryable<Data.Entities.Quotes> GetList()
        {
            IQueryable<Data.Entities.Quotes> list = new List<Data.Entities.Quotes>().AsQueryable();

            try
            {

                return _dbContext.Quotes
                    .AsNoTracking()
                    .Include(q => q.QuotesAlerts)
                    .Include(q => q.market)
                    .Include(q => q.Portfolio)
                    .Include(q => q.currency)
                    .Include(q => q.QuotesGroups);

            }
            catch (Exception ex)
            {
                return list;
            }

        }

        /// <summary>
        /// Retorna un listado de las acciones de la cache
        /// </summary>
        public async Task<List<Data.DTO.StocksDTO>> GetListAsync()
        {
            List<Data.DTO.StocksDTO> list = new List<Data.DTO.StocksDTO>();

            try
            {
                list = await _memoryCacheService.GetAsync<List<Data.DTO.StocksDTO>>("stocks");
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }


        /// <summary>
        /// Retorna una accion por id
        /// </summary>
        /// <param name="id"></param>
        public Data.Entities.Quotes GetQuote(int id)
        {
            return _dbContext.Quotes
                                .Include(q => q.market)
                                .Include(q => q.Portfolio)
                                .Include(q => q.currency)
                                .Include(q => q.QuotesAlerts)
                                .First(q => q.ID == id);

        }

        /// <summary>
        /// Retorna una accion por symbol
        /// </summary>
        /// <param name="symbol"></param>
        public Data.Entities.Quotes GetQuote(string symbol)
        {
            return _dbContext.Quotes.Include(q => q.Portfolio)
                                    .Include(q => q.QuotesGroups)
                                        .ThenInclude(qg => qg.Group)
                                    .FirstOrDefault(q => q.symbol == symbol);
        }


        /// <summary>
        /// Agrega una alerta a una accion
        /// </summary>
        public Boolean AddAlert(int quoteId, int typeId, decimal price)
        {
            try
            {
                Data.Entities.Quotes quote = _dbContext.Quotes.Include(q => q.QuotesAlerts).First(q => q.ID == quoteId);
                //Agregamos la nueva alerta
                quote.QuotesAlerts.Add(new Data.Entities.QuotesAlerts()
                {
                    QuoteId = quoteId,
                    description = String.Empty,
                    QuoteAlertTypeId = typeId,
                    price = price
                });
                //actualizamos el review de la accion
                quote.dateReview = DateTime.Now;
                _dbContext.Quotes.Update(quote);
                //actualizamos en la base de datos
                _dbContext.SaveChanges();

                //Actualizamos em la cache
                _memoryCacheService.UpdateStock(quoteId);

                return true;

            }
            catch
            {
                return false;
            }

        }


        /// <summary>
        /// Elimina una alerta de una accion
        /// </summary>
        public Boolean DeleteAlert(int quoteId, int alertId)
        {
            try
            {
                //Obtenemos la cotizacion
                Data.Entities.Quotes quote = _dbContext.Quotes.Include(q => q.QuotesAlerts).First(q => q.ID == quoteId);
                //Eliminamos el alert
                Data.Entities.QuotesAlerts alertToDelete = quote.QuotesAlerts.First(qa => qa.ID == alertId);
                _dbContext.QuotesAlerts.Remove(alertToDelete);

                //actualizamos el objeto de cotizacion
                quote.dateReview = DateTime.Now;
                _dbContext.Quotes.Update(quote);
                //actualizamos en la base de datos
                _dbContext.SaveChanges();

                //Actualizamos em la cache
                _memoryCacheService.UpdateStock(quoteId);

                return true;

            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Retorna un listado de quotes filtradas por prioridad
        /// ordenadas por el porcentaje de diferencia de precio de sus alertas
        /// </summary>
        /// <param name="priorityId"></param>
        public async Task<List<Data.DTO.StocksDTO>> GetListByPriority(int priorityId)
        {
            List<Data.DTO.StocksDTO> list = new List<Data.DTO.StocksDTO>();

            try
            {
                list = (await GetListAsync())
                            .Where(q => q.priorityId == priorityId)
                            .OrderBy(q => q._alerts.Min(qa => qa.regularMarketPercentDiff))
                            .ToList();

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }

        }

        /// <summary>
        /// Mapea un objeto quotes en su correspondiente DTO
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        public StocksDTO MapToDTO(Data.Entities.Quotes quote)
        {
            StocksDTO DTOReturn = _mapper.Map<StocksDTO>(quote);
            //Indicamos si hay que hacer una review de la accion
            DTOReturn.reviewRequired = this.HasReviewRequired(quote.regularMarketPrice, quote.QuotesAlerts.ToList());
            //Diferencia de dias desde la ultima revision
            DTOReturn.dateReviewDaysDiff = quote.dateReviewDaysDiff;

            return DTOReturn;
        }


        /// <summary>
        /// Indoca si una accion debe ser re evaluada
        /// </summary>
        /// <param name="price"></param>
        /// <param name="alertsList"></param>
        public Boolean HasReviewRequired(decimal price, List<Data.Entities.QuotesAlerts> alertsList)
        {
            Boolean hasReview = false;

            //Al menos una alerta
            if (!hasReview && !alertsList.Any())
            {
                hasReview = true;
            }

            //Tiene alguna alerta tipo RESISTENCIA menor al precio de la accion
            if (!hasReview && alertsList.Any(a => (a.QuoteAlertTypeId == 2 && (a.price < price))))
            {
                hasReview = true;
            }

            //Tiene alguna alerta tipo SOPORTE mayor al precio de la accion
            if (!hasReview && alertsList.Any(a => (a.QuoteAlertTypeId == 1 && (a.price > price))))
            {
                hasReview = true;
            }

            return hasReview;
        }

        /// <summary>
        /// Mapea una lista de quotes en su correspondiente DTO
        /// </summary>
        /// <param name="quotes"></param>
        /// <returns></returns>
        public List<StocksDTO> MapToDTO(List<Data.Entities.Quotes> quotes)
        {
            List<StocksDTO> listReturn = new List<StocksDTO>();
            quotes.ForEach(q => listReturn.Add(MapToDTO(q)));
            return listReturn;
        }

    }
}
