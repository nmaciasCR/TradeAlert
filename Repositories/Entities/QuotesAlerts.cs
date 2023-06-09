﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeAlert.Repositories.Entities
{
    public partial class QuotesAlerts
    {
        public int ID { get; set; }
        public int QuoteId { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int QuoteAlertTypeId { get; set; }

        public virtual Quotes Quote { get; set; }
        public virtual QuotesAlertsTypes QuoteAlertType { get; set; }
    }
}
