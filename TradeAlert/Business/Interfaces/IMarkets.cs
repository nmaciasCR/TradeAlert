using System.Linq;

namespace TradeAlert.Business.Interfaces
{
    public interface IMarkets
    {
        Data.Entities.Markets Get(int id);
        DTO.MarketDTO MapToDTO(Data.Entities.Markets market);
        IQueryable<Data.Entities.Markets> GetList();
    }
}
