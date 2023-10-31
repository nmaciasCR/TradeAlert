using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Business
{
    public class Portfolio : Interfaces.IPortfolio
    {
        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;


        public Portfolio(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Data.Entities.Portfolio> GetList()
        {
            List<Data.Entities.Portfolio> listPortfolio = new List<Data.Entities.Portfolio>();

            try
            {
                listPortfolio = _dbContext.Portfolio
                    .Include(p => p.quote)
                    .Include(p => p.quote.QuotesAlerts)
                    .Include(p => p.quote.market)
                    .ToList();

                return listPortfolio;


            } catch
            {
                return listPortfolio;
            }


        }


        /// <summary>
        /// Mapea un objeto portfolio en su correspondiente DTO
        /// </summary>
        public DTO.PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio)
        {
            try
            {
                DTO.PortfolioDTO DTOReturn = _mapper.Map<DTO.PortfolioDTO>(portfolio);

                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        /// <summary>
        /// Mapea una lista de quotes en su correspondiente DTO
        /// </summary>
        public List<DTO.PortfolioDTO> MapToDTO(List<Data.Entities.Portfolio> portfolioList)
        {
            List<DTO.PortfolioDTO> listReturn = new List<DTO.PortfolioDTO>();
            portfolioList.ForEach(q => listReturn.Add(MapToDTO(q)));
            return listReturn;
        }







    }
}
