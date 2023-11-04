using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Business
{
    public class Stocks : Interfaces.IStocks
    {

        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;

        public Stocks(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }


        /// <summary>
        /// Retorna un listado de las acciones
        /// </summary>
        public List<Data.Entities.Quotes> GetList()
        {
            List<Data.Entities.Quotes> list = new List<Data.Entities.Quotes>();

            try
            {
                list = _dbContext.Quotes
                    .Include(q => q.QuotesAlerts)
                    .Include(q => q.market)
                    .Include (q => q.Portfolio)
                    .ToList();

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }

        }


        public Data.Entities.Quotes GetQuote(int id)
        {
            return _dbContext.Quotes.Find(id);
        }

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

                _dbContext.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }

        }


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

                _dbContext.SaveChanges();

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
        public List<Data.Entities.Quotes> GetListByPriority(int priorityId)
        {
            List<Data.Entities.Quotes> list = new List<Data.Entities.Quotes>();

            try
            {
                list = _dbContext.Quotes
                    .Where(q => q.priorityId == priorityId)
                    .Include(q => q.market)
                    .Include(q => q.QuotesAlerts)
                    .OrderBy(q => q.QuotesAlerts.Min(qa => qa.regularMarketPercentDiff))
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
        public DTO.StocksDTO MapToDTO(Data.Entities.Quotes quote)
        {
            DTO.StocksDTO DTOReturn = _mapper.Map<DTO.StocksDTO>(quote);
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
        public List<DTO.StocksDTO> MapToDTO(List<Data.Entities.Quotes> quotes)
        {
            List< DTO.StocksDTO> listReturn = new List<DTO.StocksDTO>();
            quotes.ForEach(q => listReturn.Add(MapToDTO(q)));
            return listReturn;
        }



    }
}
