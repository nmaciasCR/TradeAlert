namespace TradeAlert.Business.Interfaces
{
    public interface ICurrency
    {
        DTO.CurrencyDTO MapToDTO(Data.Entities.Currencies currency);
    }
}
