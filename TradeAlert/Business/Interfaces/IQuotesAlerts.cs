using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface IQuotesAlerts
    {
        List<Data.Entities.QuotesAlerts> GetList(int stockId);
    }
}
