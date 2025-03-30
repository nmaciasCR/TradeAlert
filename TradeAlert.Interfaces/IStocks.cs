using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.Data.Entities;

namespace TradeAlert.Interfaces
{
    public interface IStocks
    {
        IQueryable<Data.Entities.Quotes> GetList();
        Task<List<StocksDTO>> GetListAsync();
        Data.Entities.Quotes GetQuote(int id);
        Data.Entities.Quotes GetQuote(string symbol);
        Boolean AddAlert(int quoteId, int typeId, decimal price);
        Boolean DeleteAlert(int quoteId, int alertId);
        Task<List<StocksDTO>> GetListByPriority(int priorityId);
        StocksDTO MapToDTO(Data.Entities.Quotes quote);
        Boolean HasReviewRequired(decimal price, List<Data.Entities.QuotesAlerts> alertsList);
        List<StocksDTO> MapToDTO(List<Data.Entities.Quotes> quotes);
        StocksDTO GetStockToCache(Quotes quoteToCache);
    }
}
