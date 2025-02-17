using TradeAlert.Data.Entities;

namespace TradeAlert.Data.DTO
{
    public class PortfolioDTO
    {
        public int quoteId { get; set; }
        public int quantity { get; set; }
        // Monto total de la posicion expresada en euros
        public double euroTotalAmount { get; set; }
        //Ponderancia (o peso) de esta accion respecto al portfolio (en %)
        public double weightingPercent { get; set; }
        public double averagePurchasePrice { get; set; }
        public double euroProfit { get; set; }
        public double profitPercent { get; set; }

        public StocksDTO _quote { get; set; }

    }
}
