using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TradeAlert.Data.DTO;
using TradeAlert.Business.Interfaces;

namespace TradeAlert.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly INotifications _notifications;

        public HeaderController(INotifications notifications)
        {
            _notifications = notifications;
        }


        /// <summary>
        /// Retorna la data para armar el hader
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetData")]
        public IActionResult GetData()
        {
            try
            {
                HeaderDTO headerDTO = new HeaderDTO();

                //Notificaciones activas
                headerDTO.notification = _notifications.MapToDTO(_notifications.GetList()
                                                        .Where(n => !n.deleted && n.active)
                                                        .ToList());

                return StatusCode(StatusCodes.Status200OK, headerDTO);

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
