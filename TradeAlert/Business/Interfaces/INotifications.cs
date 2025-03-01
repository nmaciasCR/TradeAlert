using System.Collections.Generic;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business.Interfaces
{
    public interface INotifications
    {
        List<Data.Entities.Notifications> GetList();
        List<Data.Entities.Notifications> GetEnabledList();
        NotificationDTO MapToDTO(Data.Entities.Notifications notification);
        List<NotificationDTO> MapToDTO(List<Data.Entities.Notifications> notificationsList);
        bool SetActive(int id, bool active);
        bool Delete(int id);
    }
}
