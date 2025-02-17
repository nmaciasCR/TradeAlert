using System;

namespace TradeAlert.Data.DTO
{
    public class CurrencyDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public double dollarExchange { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
