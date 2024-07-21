using System;
using System.ComponentModel.DataAnnotations;

namespace TradeAlert.Business.Request
{
    public class UpdatePortfolio
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int quoteId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "La cantidad de acciones debe ser mayor a 1")]
        public int quantity { get; set; }

        [Required]
        [Range(0.00001, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor a 0")]
        public double price { get; set; }

    }
}
