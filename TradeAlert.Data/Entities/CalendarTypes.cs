﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeAlert.Data.Entities
{
    public partial class CalendarTypes
    {
        public CalendarTypes()
        {
            Calendar = new HashSet<Calendar>();
        }

        public int ID { get; set; }
        public string description { get; set; }

        public virtual ICollection<Calendar> Calendar { get; set; }
    }
}
