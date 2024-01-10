using TradeAlert.Data.Entities;

namespace TradeAlert.Business.DTO
{
    public class PortfolioDTO
    {
        public int quoteId { get; set; }
        public int quantity { get; set; }
        // Monto total de la posicion expresada en euros
        public double euroTotalAmount { get; set; }
        //Ponderancia (o peso) de esta accion respecto al portfolio (en %)
        public double weightingPercent { get; set; }

        public StocksDTO _quote { get; set; }

    }
}
