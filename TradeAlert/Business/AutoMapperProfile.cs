using AutoMapper;

namespace TradeAlert.Business
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Quotes, DTO.StocksDTO>();
        }



    }
}
