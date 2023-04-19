using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
//using TradeAlert.Repositories.Entities;

namespace TradeAlert.Business
{
    public class QuotesAlerts : Interfaces.IQuotesAlerts
    {
        private Data.Entities.TradeAlertContext _dbContext;

        public QuotesAlerts(Data.Entities.TradeAlertContext dbContext)
        {
            this._dbContext = dbContext;
        }


         public List<Data.Entities.QuotesAlerts> GetList(int stockId)
        {
            List<Data.Entities.QuotesAlerts> list = new();

            try
            {
                list = this._dbContext.QuotesAlerts.Where(qa => qa.QuoteId == stockId).ToList();
                return list;

            } catch (Exception ex)
            {
                return list;
            }

        }


    }
}
