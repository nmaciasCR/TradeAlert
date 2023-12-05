using System;
using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface IPortfolio
    {

        List<Data.Entities.Portfolio> GetList();
        DTO.PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio);
        List<DTO.PortfolioDTO> MapToDTO(List<Data.Entities.Portfolio> portfolioList);
        Boolean Update(Request.UpdatePortfolio pStock);
        Boolean Delete(int idPortfolio);

    }
}
