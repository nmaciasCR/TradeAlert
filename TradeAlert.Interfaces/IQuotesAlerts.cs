using System.Collections.Generic;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.Interfaces
{
    public interface IQuotesAlerts
    {
        Task<List<Data.DTO.QuotesAlertsDTO>> GetList(int stockId);
        QuotesAlertsDTO MapToDTO(Data.Entities.QuotesAlerts alert);
        List<QuotesAlertsDTO> MapToDTO(List<Data.Entities.QuotesAlerts> alerts);
    }
}
