using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TradeAlert.Interfaces;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business
{
    public class QuotesAlerts : IQuotesAlerts
    {
        private readonly Lazy<IStocks> _businessStocks;
        private readonly IMapper _mapper;


        public QuotesAlerts(Lazy<IStocks> businessStocks, IMapper mapper)
        {
            _businessStocks = businessStocks;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtenemos la coleccion de alertas de una accion de la cache
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<List<Data.DTO.QuotesAlertsDTO>> GetList(int stockId)
        {
            var _bsStocks = _businessStocks.Value;
            List<Data.DTO.QuotesAlertsDTO> list = new();
            try
            {
                return (await _bsStocks.GetListAsync())
                    .First(s => s.ID == stockId)
                    ._alerts;

            } catch (Exception ex)
            {
                return list;
            }
        }

        /// <summary>
        /// Mapea un objeto QuotesAlerts en su correspondiente DTO
        /// </summary>
        public QuotesAlertsDTO MapToDTO(Data.Entities.QuotesAlerts alert)
        {
            try
            {
                QuotesAlertsDTO DTOReturn = _mapper.Map<QuotesAlertsDTO>(alert);
                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Mapea un listado de objetos QuotesAlerts en su correspondiente DTO
        /// </summary>
        public List<QuotesAlertsDTO> MapToDTO(List<Data.Entities.QuotesAlerts> alerts)
        {
            try
            {
                List<QuotesAlertsDTO> DTOReturn = alerts.Select(a => MapToDTO(a)).ToList();
                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
