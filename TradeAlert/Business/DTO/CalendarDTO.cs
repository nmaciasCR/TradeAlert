using System;

namespace TradeAlert.Business.DTO
{
    public class CalendarDTO
    {
        public int ID { get; set; }
        public int calendarTypeId { get; set; }
        public string description { get; set; }
        public DateTime scheduleDate { get; set; }
        public DateTime entryDate { get; set; }
        public bool deleted { get; set; }

    }
}
