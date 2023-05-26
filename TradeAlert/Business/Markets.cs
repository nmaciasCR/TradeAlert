using AutoMapper;

namespace TradeAlert.Business
{
    public class Markets : Interfaces.IMarkets
    {

        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;



        public Markets(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;

        }



        public DTO.MarketDTO MapToDTO(Data.Entities.Markets market)
        {
            DTO.MarketDTO dtoReturn = _mapper.Map<DTO.MarketDTO>(market);

            return dtoReturn;
        }



    }
}
