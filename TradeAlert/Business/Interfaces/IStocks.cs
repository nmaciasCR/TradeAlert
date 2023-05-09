using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Business.Interfaces
{
    public interface IStocks
    {
        List<Data.Entities.Quotes> GetList();
        Data.Entities.Quotes GetQuote(int id);
        Boolean AddAlert(int quoteId, int typeId, decimal price);
        Boolean DeleteAlert(int quoteId, int alertId);
        List<Data.Entities.Quotes> GetListByPriority(int priorityId);
        DTO.StocksDTO MapToDTO(Data.Entities.Quotes quote);
        public Boolean HasReviewRequired(decimal price, List<Data.Entities.QuotesAlerts> alertsList);
    }
}
