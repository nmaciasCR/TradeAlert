using System.Collections.Generic;
using TradeAlert.Business.DTO;

namespace TradeAlert.Business.Request
{
    public class UpdateQuoteGroups
    {
        public int quoteId { get; set; }
        public List<GroupDTO> groupList { get; set; }
    }
}
