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
    }
}
