using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface IGroups
    {
        List<Data.Entities.Groups> GetAll();
        Data.DTO.GroupDTO MapToDTO(Data.Entities.Groups group);
        List<Data.DTO.GroupDTO> MapToDTO(List<Data.Entities.Groups> groups);
        List<Data.Entities.Groups> GetByStock(int stockId);
        bool Update(int quoteId, List<Data.DTO.GroupDTO> list);

    }
}
