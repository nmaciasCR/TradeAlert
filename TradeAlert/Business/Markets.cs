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

        /// <summary>
        /// Retorna un objeto Market por ID
        /// </summary>
        /// <param name="id"></param>
        public Data.Entities.Markets Get(int id)
        {
            return _dbContext.Markets.Find(id);
        }

        public DTO.MarketDTO MapToDTO(Data.Entities.Markets market)
        {
            DTO.MarketDTO dtoReturn = _mapper.Map<DTO.MarketDTO>(market);

            return dtoReturn;
        }



    }
}
