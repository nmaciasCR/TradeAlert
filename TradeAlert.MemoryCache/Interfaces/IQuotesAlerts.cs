using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Interfaces
{
    public interface IQuotesAlerts
    {
        QuotesAlertsDTO MapToDTO(Data.Entities.QuotesAlerts alert);
        List<QuotesAlertsDTO> MapToDTO(List<Data.Entities.QuotesAlerts> alerts);
    }
}
