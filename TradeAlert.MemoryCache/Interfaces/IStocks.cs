using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Interfaces
{
    public interface IStocks
    {
        Data.Entities.Quotes Get(int id);
        IQueryable<Data.Entities.Quotes> GetList();
        StocksDTO MapToDTO(Data.Entities.Quotes quote);

    }
}
