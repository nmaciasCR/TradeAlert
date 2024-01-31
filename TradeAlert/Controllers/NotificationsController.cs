using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        private readonly INotifications _businessNotifications;



        public NotificationsController(INotifications businessNotifications)
        {
            _businessNotifications = businessNotifications;
        }


        /// <summary>
        /// Retorna un listado de notificaciones habilitadas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEnabledNotifications")]
        public IActionResult GetEnabledNotifications() {
        
            List<Data.Entities.Notifications> notificationList;
            try
            {
                notificationList = _businessNotifications
                                    .GetEnabledList()
                                    .OrderByDescending(n => n.entryDate)
                                    .ToList();
                return StatusCode(StatusCodes.Status200OK, _businessNotifications.MapToDTO(notificationList));
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL OBTENER NOTIFICACIONES HABILITADAS");
            }
        
        }

        /// <summary>
        /// Cambia el estado activo / no activo de una notificacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetActive")]
        public IActionResult SetActive(int id, bool active)
        {
            try
            {
                _businessNotifications.SetActive(id, active);
                return StatusCode(StatusCodes.Status200OK);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL CAMBIAR EL ESTADO ACTIVO DE LA NOTIFICACION");
            }
        }

        /// <summary>
        /// Elimina logicamente una notificacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _businessNotifications.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR 500: ERROR AL ELIMINAR UNA NOTIFICACION");
            }
        }

    }
}
