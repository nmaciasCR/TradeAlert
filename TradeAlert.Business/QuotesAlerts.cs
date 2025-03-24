using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Business
{
    public class QuotesAlerts : Interfaces.IQuotesAlerts
    {
        private readonly IStocks _businessStocks;


        public QuotesAlerts(IStocks businessStocks)
        {
            _businessStocks = businessStocks;
        }

        /// <summary>
        /// Obtenemos la coleccion de alertas de una accion de la cache
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<List<Data.DTO.QuotesAlertsDTO>> GetList(int stockId)
        {
            List<Data.DTO.QuotesAlertsDTO> list = new();
            try
            {
                return (await _businessStocks.GetListAsync())
                    .First(s => s.ID == stockId)
                    ._alerts;

            } catch (Exception ex)
            {
                return list;
            }
        }

    }
}
