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
    public class Portfolio : IPortfolio
    {

        private readonly IMapper _mapper;


        public Portfolio(IMapper mapper) { 
            _mapper = mapper;
        
        }


        /// <summary>
        /// Mapea un objeto portfolio en su correspondiente DTO
        /// </summary>
        public PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio)
        {
            try
            {
                PortfolioDTO DTOReturn = _mapper.Map<PortfolioDTO>(portfolio);
                DTOReturn.euroTotalAmount = portfolio.euroTotalAmount;

                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }



    }
}
