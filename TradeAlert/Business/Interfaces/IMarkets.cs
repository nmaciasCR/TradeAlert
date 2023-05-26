namespace TradeAlert.Business.Interfaces
{
    public interface IMarkets
    {
        DTO.MarketDTO MapToDTO(Data.Entities.Markets market);
    }
}
