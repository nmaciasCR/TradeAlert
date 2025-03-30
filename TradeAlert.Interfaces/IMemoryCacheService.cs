using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeAlert.Interfaces
{
    public interface IMemoryCacheService
    {

        Task SetAsync<T>(string key, T value, TimeSpan duration);
        Task<T?> GetAsync<T>(string key);
        Task<int> RefreshStocksAllCache();
        Task<bool> UpdateStock(int quoteId);
    }
}
