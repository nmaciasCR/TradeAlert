using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.DTO;
using TradeAlert.Business.Request;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {

        private readonly Business.Interfaces.IGroups _BusinessGroups;



        public GroupsController(Business.Interfaces.IGroups businessGroups)
        {
            _BusinessGroups = businessGroups;
        }

        //Retorna todos los grupos de la DDBB
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            try
            {
                List<GroupDTO> groups = new List<GroupDTO>();

                foreach (Data.Entities.Groups group in _BusinessGroups.GetAll())
                {
                    GroupDTO groupDTO = _BusinessGroups.MapToDTO(group);
                    groupDTO.quoteQty = group.QuotesGroups.Count;
                    groups.Add(groupDTO);
                }

                return StatusCode(StatusCodes.Status200OK, groups);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        //Retorna los grupos seleccionados de una accion
        [HttpGet]
        [Route("GetSelectedList")]
        public IActionResult GetSelectedList(int q)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _BusinessGroups.MapToDTO(_BusinessGroups.GetByStock(q)));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Actualiza los grupos asignados a una accion
        /// </summary>
        [HttpPut]
        [Route("UpdateList")]
        public IActionResult UpdateList([FromBody] UpdateQuoteGroups body)
        {
            try
            {
                //Extraemos los datos del body
                int quoteId = body.quoteId;
                List<GroupDTO> groupList = body.groupList;

                _BusinessGroups.Update(quoteId, groupList);
                return StatusCode(StatusCodes.Status200OK);

            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }
    }
}
