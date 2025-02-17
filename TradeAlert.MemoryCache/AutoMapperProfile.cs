using AutoMapper;

namespace TradeAlert.MemoryCache
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Quotes, Data.DTO.StocksDTO>();
            CreateMap<Data.Entities.Markets, Data.DTO.MarketDTO>();
            CreateMap<Data.Entities.Portfolio, Data.DTO.PortfolioDTO>();
            CreateMap<Data.Entities.Currencies, Data.DTO.CurrencyDTO>();
            CreateMap<Data.Entities.Notifications, Data.DTO.NotificationDTO>();
            CreateMap<Data.Entities.Calendar, Data.DTO.CalendarDTO>();
            CreateMap<Data.Entities.Groups, Data.DTO.GroupDTO>();
            CreateMap<Data.Entities.QuotesAlerts, Data.DTO.QuotesAlertsDTO>();
        }



    }
}
