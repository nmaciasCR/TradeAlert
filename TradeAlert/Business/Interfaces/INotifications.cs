using System.Collections.Generic;

namespace TradeAlert.Business.Interfaces
{
    public interface INotifications
    {
        List<Data.Entities.Notifications> GetList();
        List<Data.Entities.Notifications> GetEnabledList();
        DTO.NotificationDTO MapToDTO(Data.Entities.Notifications notification);
        List<DTO.NotificationDTO> MapToDTO(List<Data.Entities.Notifications> notificationsList);
        bool SetActive(int id, bool active);
        bool Delete(int id);
    }
}
