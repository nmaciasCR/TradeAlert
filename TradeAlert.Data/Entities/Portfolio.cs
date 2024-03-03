﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeAlert.Data.Entities
{
    public partial class Portfolio
    {
        public int quoteId { get; set; }
        public int quantity { get; set; }
        public double averagePurchasePrice { get; set; }

        public virtual Quotes quote { get; set; }
    }
}
