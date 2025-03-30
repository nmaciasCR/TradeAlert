using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Interfaces;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business
{
    public class Notifications : INotifications
    {
        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;

        public Notifications(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }


        /// <summary>
        /// Retorna un listado de todas las notificaciones
        /// </summary>
        /// <returns></returns>
        public List<Data.Entities.Notifications> GetList()
        {
            List<Data.Entities.Notifications> listReturn = new List<Data.Entities.Notifications>();

            try
            {
                listReturn = _dbContext.Notifications.ToList();
                return listReturn;

            } catch
            {
                return listReturn;
            }
        }


        /// <summary>
        /// Retorna un listado de notificaciones no eliminadas logicamente
        /// </summary>
        /// <returns></returns>
        public List<Data.Entities.Notifications> GetEnabledList()
        {
            return GetList()
                .Where(n => !n.deleted)
                .ToList();
        }


        /// <summary>
        /// Mapea un objeto notification en su correspondiente DTO
        /// </summary>
        public NotificationDTO MapToDTO(Data.Entities.Notifications notification)
        {
            try
            {
                NotificationDTO DTOReturn = _mapper.Map<NotificationDTO>(notification);
                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapea una lista de notifiaciones en su correspondiente DTO
        /// </summary>
        public List<NotificationDTO> MapToDTO(List<Data.Entities.Notifications> notificationsList)
        {
            List<NotificationDTO> listReturn = new List<NotificationDTO>();
            notificationsList.ForEach(n => listReturn.Add(MapToDTO(n)));
            return listReturn;
        }

        /// <summary>
        /// Setea el estado de active de una notificacion
        /// </summary>
        public bool SetActive(int id, bool active)
        {
            try
            {
                _dbContext.Notifications.Find(id).active = active;
                _dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }


        /// <summary>
        /// Elimina logicamente una notificacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                _dbContext.Notifications.Find(id).deleted = true;
                _dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

    }
}
