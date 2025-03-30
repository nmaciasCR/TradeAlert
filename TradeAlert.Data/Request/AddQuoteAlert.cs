namespace TradeAlert.Data.Request
{
    public class AddQuoteAlert
    {
        public int QuoteId { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }
    }
}
