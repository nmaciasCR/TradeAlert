using System.Collections.Generic;


namespace TradeAlert.Data.Request
{
    public class UpdateQuoteGroups
    {
        public int quoteId { get; set; }
        public List<DTO.GroupDTO> groupList { get; set; }
    }
}
