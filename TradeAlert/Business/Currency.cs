using AutoMapper;
using System.Linq;

namespace TradeAlert.Business
{
    public class Currency : Interfaces.ICurrency
    {
        private readonly IMapper _mapper;
        private Data.Entities.TradeAlertContext _dbContext;

        public Currency(IMapper mapper, Data.Entities.TradeAlertContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retorna un objeto currency
        /// </summary>
        public Data.Entities.Currencies Get(int id)
        {
            return _dbContext.Currencies.Find(id);
        }

        /// <summary>
        /// Mapea un objeto currency en su correspondiente DTO
        /// </summary>
        public DTO.CurrencyDTO MapToDTO(Data.Entities.Currencies currency)
        {
            DTO.CurrencyDTO DTOReturn = _mapper.Map<DTO.CurrencyDTO>(currency);
            return DTOReturn;
        }

    }
}
