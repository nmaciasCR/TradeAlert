using System;

namespace TradeAlert.Business.DTO
{
    public class StocksDTO
    {
        public int ID { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public int marketId { get; set; }
        public string currency { get; set; }
        public decimal regularMarketPrice { get; set; }
        public decimal? regularMarketChangePercent { get; set; }
        public DateTime? updateDate { get; set; }
        public int priorityId { get; set; }
        public decimal? regularMarketChange { get; set; }
        public DateTime dateReview { get; set; }
        //Indica si la accion debe ser re-evaluada
        public bool reviewRequired { get; set; }


    }
}
