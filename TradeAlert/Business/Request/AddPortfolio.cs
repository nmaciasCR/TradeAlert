using System.ComponentModel.DataAnnotations;
using System;

namespace TradeAlert.Business.Request
{
    public class AddPortfolio
    {

        [Required]
        [Range(1, Int32.MaxValue)]
        public int quoteId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "La cantidad de acciones debe ser al menos 1")]
        public int quantity { get; set; }

        [Required]
        public double price { get; set; }

    }
}
