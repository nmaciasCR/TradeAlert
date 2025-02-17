using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Business
{


    public class Stocks : Interfaces.IStocks
    {
        private readonly IMapper _mapper;
        private Data.Entities.TradeAlertContext _dbContext;


        public Stocks(IMapper mapper, Data.Entities.TradeAlertContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retorna un stock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Entities.Quotes Get(int id)
        {
            return _dbContext.Quotes
                                .AsNoTracking()
                                .Include(q => q.QuotesAlerts)
                                .Include(q => q.market)
                                .Include(q => q.Portfolio)
                                .Include(q => q.currency)
                                .Include(q => q.QuotesGroups)
                                .Include(q => q.QuotesAlerts)
                                .First(q => q.ID == id);
        }

        /// <summary>
        /// Retorna un listado de stocks AsNoTracking
        /// </summary>
        /// <returns></returns>
        public IQueryable<Data.Entities.Quotes> GetList()
        {

            return _dbContext.Quotes
                                .AsNoTracking()
                                .Include(q => q.QuotesAlerts)
                                .Include(q => q.market)
                                .Include(q => q.Portfolio)
                                .Include(q => q.currency)
                                .Include(q => q.QuotesGroups)
                                .Include(q => q.QuotesAlerts);

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
            DTOReturn.reviewRequired = HasReviewRequired(quote.regularMarketPrice, quote.QuotesAlerts.ToList());
            //Diferencia de dias desde la ultima revision
            DTOReturn.dateReviewDaysDiff = quote.dateReviewDaysDiff;

            return DTOReturn;
        }


        /// <summary>
        /// Indoca si una accion debe ser re evaluada
        /// </summary>
        /// <param name="price"></param>
        /// <param name="alertsList"></param>
        public bool HasReviewRequired(decimal price, List<Data.Entities.QuotesAlerts> alertsList)
        {
            bool hasReview = false;

            //Al menos una alerta
            if (!hasReview && !alertsList.Any())
            {
                hasReview = true;
            }

            //Tiene alguna alerta tipo RESISTENCIA menor al precio de la accion
            if (!hasReview && alertsList.Any(a => a.QuoteAlertTypeId == 2 && a.price < price))
            {
                hasReview = true;
            }

            //Tiene alguna alerta tipo SOPORTE mayor al precio de la accion
            if (!hasReview && alertsList.Any(a => a.QuoteAlertTypeId == 1 && a.price > price))
            {
                hasReview = true;
            }

            return hasReview;
        }




    }
}
