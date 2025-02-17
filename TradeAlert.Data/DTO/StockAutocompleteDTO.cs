using System.Reflection.Metadata.Ecma335;

namespace TradeAlert.Data.DTO
{
    public class StockAutocompleteDTO
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }

    }
}
