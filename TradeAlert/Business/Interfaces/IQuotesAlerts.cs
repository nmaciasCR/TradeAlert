using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeAlert.Business.Interfaces
{
    public interface IQuotesAlerts
    {
        Task<List<Data.DTO.QuotesAlertsDTO>> GetList(int stockId);
    }
}
