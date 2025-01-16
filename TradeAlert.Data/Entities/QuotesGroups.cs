﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeAlert.Data.Entities
{
    public partial class QuotesGroups
    {
        public int QuoteId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Quotes Quote { get; set; }
    }
}
