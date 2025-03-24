using AutoMapper;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Quotes, StocksDTO>();
            CreateMap<Data.Entities.Markets, MarketDTO>();
            CreateMap<Data.Entities.Portfolio, PortfolioDTO>();
            CreateMap<Data.Entities.Currencies, CurrencyDTO>();
            CreateMap<Data.Entities.Notifications, NotificationDTO>();
            CreateMap<Data.Entities.Calendar, CalendarDTO>();
            CreateMap<Data.Entities.Groups, GroupDTO>();
        }



    }
}
