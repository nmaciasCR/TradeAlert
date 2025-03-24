using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business.Interfaces
{
    public interface IPortfolio
    {

        List<Data.Entities.Portfolio> GetList();
        Task<List<StocksDTO>> GetListFromCache();
        PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio);
        List<PortfolioDTO> MapToDTO(List<Data.Entities.Portfolio> portfolioList);
        ProblemDetails Update(Request.UpdatePortfolio pStock);
        Boolean Delete(int idPortfolio);
        double GetWeightingPercent(double portfolioAmount, double stockAmount);
        ProblemDetails Add(Request.UpdatePortfolio addPortfolio);
    }
}
