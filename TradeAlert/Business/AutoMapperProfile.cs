using AutoMapper;

namespace TradeAlert.Business
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Quotes, DTO.StocksDTO>();
            CreateMap<Data.Entities.Markets, DTO.MarketDTO>();
            CreateMap<Data.Entities.Portfolio, DTO.PortfolioDTO>();
            CreateMap<Data.Entities.Currencies, DTO.CurrencyDTO>();
            CreateMap<Data.Entities.Notifications, DTO.NotificationDTO>();
            CreateMap<Data.Entities.Calendar, DTO.CalendarDTO>();
            CreateMap<Data.Entities.Groups, DTO.GroupDTO>();
        }



    }
}
