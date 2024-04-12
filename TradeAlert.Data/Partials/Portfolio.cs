using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TradeAlert.Data.Entities
{
    public partial class Portfolio
    {

        //Monto total de la posicion expresada en euros
        public double euroTotalAmount
        {
            get
            {
                int LIBRA_ESTERLINA_ID = 4;
                decimal currentPrice = this.quote.regularMarketPrice;
                //Acciones del reino unido estan en centavos
                //La convertinos a libra esterlina
                if (this.quote.currency.ID == LIBRA_ESTERLINA_ID)
                {
                    currentPrice = currentPrice / 100;
                }
                return (double)(this.quantity * currentPrice) * this.quote.currency.euroExchange;
            }
        }

        //Ganancia
        public double profit
        {
            get
            {
                return this.quantity * ((double)this.quote.regularMarketPrice - this.averagePurchasePrice);
            }
        }

        //Porcentaje de ganancia
        public double profitPercent
        {
            get
            {
                return (((double)this.quote.regularMarketPrice / this.averagePurchasePrice) -1) * 100;
            }
        }


    }
}
