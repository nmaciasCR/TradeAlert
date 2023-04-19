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

        public Stocks(Data.Entities.TradeAlertContext dbContext)
        {
            this._dbContext = dbContext;
        }


        /// <summary>
        /// Retorna un listado de las acciones
        /// </summary>
        public List<Data.Entities.Quotes> GetList()
        {
            List<Data.Entities.Quotes> list = new List<Data.Entities.Quotes>();

            try
            {
                list = _dbContext.Quotes.ToList();

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
                quote.QuotesAlerts.Add(new Data.Entities.QuotesAlerts()
                {
                    QuoteId = quoteId,
                    description = String.Empty,
                    QuoteAlertTypeId = typeId,
                    price = price
                });

                _dbContext.Quotes.Update(quote);
                _dbContext.SaveChanges();

                return true;

            } catch
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

                _dbContext.SaveChanges();

                return true;

            } catch
            {
                return false;
            }
        }


    }
}
