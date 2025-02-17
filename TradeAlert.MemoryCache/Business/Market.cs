using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.MemoryCache.Interfaces;

namespace TradeAlert.MemoryCache.Business
{
    public class Market : IMarkets
    {
        private readonly IMapper _mapper;


        public Market(IMapper mapper)
        {
            _mapper = mapper;
        }
        public MarketDTO MapToDTO(Data.Entities.Markets market)
        {
            MarketDTO dtoReturn = _mapper.Map<MarketDTO>(market);

            return dtoReturn;
        }

    }
}
