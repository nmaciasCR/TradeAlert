using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Business
{
    public class Calendar : Interfaces.ICalendar
    {

        private readonly IMapper _mapper;
        private Data.Entities.TradeAlertContext _dbContext;

        public Calendar(IMapper mapper, Data.Entities.TradeAlertContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retorna un listado de las fechas del calendario habilitadas
        /// </summary>
        /// <returns></returns>
        public List<Data.Entities.Calendar> GetCurrentList()
        {
            return _dbContext.Calendar.Where(c => !c.deleted && c.scheduleDate.Date >= DateTime.Now.Date)
                                      .ToList();        

        }

        /// <summary>
        /// Mapea un objeto calendar en su correspondiente DTO
        /// </summary>
        public Data.DTO.CalendarDTO MapToDTO(Data.Entities.Calendar calendar)
        {
            Data.DTO.CalendarDTO DTOReturn = _mapper.Map<Data.DTO.CalendarDTO>(calendar);
            return DTOReturn;
        }


    }
}
