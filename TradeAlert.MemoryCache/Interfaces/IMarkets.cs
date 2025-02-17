using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Interfaces
{
    public interface IMarkets
    {
        MarketDTO MapToDTO(Data.Entities.Markets market);
    }
}
