using System.Linq;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business.Interfaces
{
    public interface IMarkets
    {
        Data.Entities.Markets Get(int id);
        MarketDTO MapToDTO(Data.Entities.Markets market);
        IQueryable<Data.Entities.Markets> GetList();
    }
}
