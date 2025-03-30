using System.Collections.Generic;

namespace TradeAlert.Interfaces
{
    public interface ICalendar
    {

        List<Data.Entities.Calendar> GetCurrentList();
        Data.DTO.CalendarDTO MapToDTO(Data.Entities.Calendar calendar);

    }
}
