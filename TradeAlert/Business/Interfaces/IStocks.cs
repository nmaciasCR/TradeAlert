﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Business.Interfaces
{
    public interface IStocks
    {
        IQueryable<Data.Entities.Quotes> GetList();
        Task<List<Data.DTO.StocksDTO>> GetListAsync();
        Data.Entities.Quotes GetQuote(int id);
        Data.Entities.Quotes GetQuote(string symbol);
        Boolean AddAlert(int quoteId, int typeId, decimal price);
        Boolean DeleteAlert(int quoteId, int alertId);
        Task<List<Data.DTO.StocksDTO>> GetListByPriority(int priorityId);
        DTO.StocksDTO MapToDTO(Data.Entities.Quotes quote);
        Boolean HasReviewRequired(decimal price, List<Data.Entities.QuotesAlerts> alertsList);
        List<DTO.StocksDTO> MapToDTO(List<Data.Entities.Quotes> quotes);
        Task<bool> UpdateInCache(int quoteId);
    }
}
