using System;
using System.Collections.Generic;

namespace TradeAlert.Data.DTO
{
    public class StocksDTO
    {
        public int ID { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public int marketId { get; set; }
        public int currencyId { get; set; }
        public decimal regularMarketPrice { get; set; }
        public decimal? regularMarketChangePercent { get; set; }
        public DateTime? updateDate { get; set; }
        public int priorityId { get; set; }
        public decimal? regularMarketChange { get; set; }
        public DateTime dateReview { get; set; }
        //Indica si la accion debe ser re-evaluada
        public bool reviewRequired { get; set; }
        //Dias desde la ultima revision
        public int dateReviewDaysDiff { get; set; }


        public MarketDTO _market { get; set; }
        public PortfolioDTO _Portfolio { get; set; }
        public CurrencyDTO _currency { get; set; }
        public List<GroupDTO> _groups { get; set; }
        public List<int> groupsIdList { get; set; }
        public List<QuotesAlertsDTO> _alerts { get; set; }
    }
}
