using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeAlert.Business.Interfaces
{
    public interface IStocks
    {
        List<Data.Entities.Quotes> GetList();

    }
}
