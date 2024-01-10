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
                return (double)(this.quantity * this.quote.regularMarketPrice) * this.quote.currency.euroExchange;
            }
        }




    }
}
