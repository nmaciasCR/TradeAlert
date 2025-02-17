using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;

namespace TradeAlert.MemoryCache.Interfaces
{
    public interface ICurrencies
    {

        CurrencyDTO MapToDTO(Data.Entities.Currencies currency);
    }
}
