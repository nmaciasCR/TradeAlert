namespace TradeAlert.Business.Interfaces
{
    public interface ICurrency
    {
        Data.Entities.Currencies Get(int id);
        DTO.CurrencyDTO MapToDTO(Data.Entities.Currencies currency);
    }
}
