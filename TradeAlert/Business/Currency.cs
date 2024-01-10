using AutoMapper;
using System.Linq;

namespace TradeAlert.Business
{
    public class Currency : Interfaces.ICurrency
    {
        private readonly IMapper _mapper;

        public Currency(IMapper mapper)
        {
            _mapper = mapper;
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
