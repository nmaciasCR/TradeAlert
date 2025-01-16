using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface IGroups
    {
        List<Data.Entities.Groups> GetAll();
        DTO.GroupDTO MapToDTO(Data.Entities.Groups group);
        List<DTO.GroupDTO> MapToDTO(List<Data.Entities.Groups> groups);
        List<Data.Entities.Groups> GetByStock(int stockId);
        bool Update(int quoteId, List<DTO.GroupDTO> list);

    }
}
