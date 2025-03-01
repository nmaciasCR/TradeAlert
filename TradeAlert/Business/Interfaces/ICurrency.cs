namespace TradeAlert.Business.Interfaces
{
    public interface ICurrency
    {
        Data.Entities.Currencies Get(int id);
        Data.DTO.CurrencyDTO MapToDTO(Data.Entities.Currencies currency);
    }
}
