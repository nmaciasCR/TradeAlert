using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Business
{
    public class Stocks : Interfaces.IStocks
    {

        private Repositories.Entities.TradeAlertContext _dbContext;

        public Stocks(Repositories.Entities.TradeAlertContext dbContext)
        {
            this._dbContext = dbContext;
        }


        /// <summary>
        /// Retorna un listado de las acciones
        /// </summary>
        public List<Repositories.Entities.Quotes> GetList()
        {
            List<Repositories.Entities.Quotes> list = new List<Repositories.Entities.Quotes>();

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
    }
}
