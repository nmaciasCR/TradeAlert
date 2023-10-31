using TradeAlert.Data.Entities;

namespace TradeAlert.Business.DTO
{
    public class PortfolioDTO
    {

        public int ID { get; set; }
        public int quoteId { get; set; }
        public int quantity { get; set; }

        public StocksDTO _quote { get; set; }

    }
}
