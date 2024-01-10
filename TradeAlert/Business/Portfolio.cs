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
                    .Include(p => p.quote.currency)
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
                DTOReturn.euroTotalAmount = portfolio.euroTotalAmount;

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


        /// <summary>
        /// Actualiza los datos de una accion del portfolio
        /// </summary>
        public Boolean Update(Request.UpdatePortfolio pStock)
        {
            try
            {
                _dbContext.Portfolio.Find(pStock.quoteId).quantity = pStock.quantity;
                _dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }

        }

        /// <summary>
        /// Eliminar una accion del portfolio
        /// </summary>
        public Boolean Delete(int idPortfolio)
        {
            try
            {
                Data.Entities.Portfolio portfolioToDelete = _dbContext.Portfolio.Find(idPortfolio);
                _dbContext.Portfolio.Remove(portfolioToDelete);
                _dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retorna el porcentaje que representa una accion
        /// respecto al portfolio
        /// </summary>
        public double GetWeightingPercent(double portfolioAmount, double stockAmount)
        {
            return (stockAmount / portfolioAmount) * 100;
        }



    }
}
