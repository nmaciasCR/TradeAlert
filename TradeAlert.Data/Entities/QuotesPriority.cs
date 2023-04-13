﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeAlert.Data.Entities
{
    public partial class QuotesPriority
    {
        public QuotesPriority()
        {
            Quotes = new HashSet<Quotes>();
        }

        public int ID { get; set; }
        public string name { get; set; }

        public virtual ICollection<Quotes> Quotes { get; set; }
    }
}
