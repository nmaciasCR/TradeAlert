using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.MemoryCache.Interfaces;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Business
{
    public class Currencies : ICurrencies
    {

        private readonly IMapper _mapper;

        public Currencies(IMapper mapper) { 
            _mapper = mapper;
        }


        /// <summary>
        /// Mapea un objeto currency en su correspondiente DTO
        /// </summary>
        public CurrencyDTO MapToDTO(Data.Entities.Currencies currency)
        {
            CurrencyDTO DTOReturn = _mapper.Map<CurrencyDTO>(currency);
            return DTOReturn;
        }



    }
}
