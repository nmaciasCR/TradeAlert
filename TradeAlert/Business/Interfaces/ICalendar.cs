using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface ICalendar
    {

        List<Data.Entities.Calendar> GetCurrentList();
        Data.DTO.CalendarDTO MapToDTO(Data.Entities.Calendar calendar);

    }
}
