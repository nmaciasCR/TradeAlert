using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeAlert.Data.DTO
{
    public class QuotesAlertsDTO
    {
        public int ID { get; set; }
        public int QuoteId { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int QuoteAlertTypeId { get; set; }
        public decimal regularMarketPercentDiff { get; set; }
    }
}
