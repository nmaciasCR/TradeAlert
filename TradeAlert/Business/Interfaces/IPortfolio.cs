﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeAlert.Business.Interfaces
{
    public interface IPortfolio
    {

        List<Data.Entities.Portfolio> GetList();
        Task<List<Data.DTO.StocksDTO>> GetListFromCache();
        DTO.PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio);
        List<DTO.PortfolioDTO> MapToDTO(List<Data.Entities.Portfolio> portfolioList);
        ProblemDetails Update(Request.UpdatePortfolio pStock);
        Boolean Delete(int idPortfolio);
        double GetWeightingPercent(double portfolioAmount, double stockAmount);
        ProblemDetails Add(Request.UpdatePortfolio addPortfolio);
    }
}
