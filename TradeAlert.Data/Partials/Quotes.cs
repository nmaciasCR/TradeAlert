using System;

namespace TradeAlert.Data.Entities
{
    public partial class Quotes
    {

        ///Property que devuelve los dias desde la ultima revision de la accion
        public int dateReviewDaysDiff
        {
            get
            {
                return (DateTime.Now - this.dateReview).Days;
            }
        }


    }
}
