using System.Collections.Generic;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business.Request
{
    public class UpdateQuoteGroups
    {
        public int quoteId { get; set; }
        public List<GroupDTO> groupList { get; set; }
    }
}
