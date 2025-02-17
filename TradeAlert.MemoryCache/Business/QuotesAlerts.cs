using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.MemoryCache.Interfaces;

namespace TradeAlert.MemoryCache.Business
{
    public class QuotesAlerts : IQuotesAlerts
    {
        private readonly IMapper _mapper;

        public QuotesAlerts(IMapper mapper)
        {
            _mapper = mapper;
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
