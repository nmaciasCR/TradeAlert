using System;

namespace TradeAlert.Data.Entities
{
    public partial class Quotes
    {

        ///Property que devuelva la diferencia en dias entre dos fechas
        public int dateReviewDaysDiff
        {
            get
            {
                return (DateTime.Now - this.dateReview).Days;
            }
        }


    }
}
