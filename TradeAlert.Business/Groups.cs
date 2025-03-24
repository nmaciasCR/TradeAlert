using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Data.DTO;

namespace TradeAlert.Business
{
    public class Groups : Interfaces.IGroups
    {

        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;

        public Groups(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna un listado de todos los grupos disponibles en la DDBB
        /// </summary>
        /// <returns></returns>
        public List<Data.Entities.Groups> GetAll()
        {
            return _dbContext.Groups
                                .Include(g => g.QuotesGroups)
                                .OrderBy(g => g.description)
                                .ToList();
        }

        /// <summary>
        /// Mapea un objeto Groups en su correspondiente DTO
        /// </summary>
        /// <param name="Groups"></param>
        /// <returns></returns>
        public GroupDTO MapToDTO(Data.Entities.Groups group)
        {
            GroupDTO DTOReturn = _mapper.Map<GroupDTO>(group);

            return DTOReturn;
        }

        /// <summary>
        /// Mapea una lista de Groups en su correspondiente DTO
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public List<GroupDTO> MapToDTO(List<Data.Entities.Groups> groups)
        {
            List<GroupDTO> listReturn = groups.Select(g => MapToDTO(g)).ToList();
            return listReturn;
        }

        /// <summary>
        /// Retorna un listado de los grupos seleccionados de una accion
        /// </summary>
        /// <returns></returns>
        public List<Data.Entities.Groups> GetByStock(int stockId)
        {
            return _dbContext.Groups
                                .Include(g => g.QuotesGroups)
                                .Where(g => g.QuotesGroups.Any(qg => qg.QuoteId == stockId))
                                .ToList();
        }

        /// <summary>
        /// Actualizamos los grupos asignamos a una accion
        /// </summary>
        public bool Update(int quoteId, List<GroupDTO> list)
        {
            try
            {
                //Eliminamos los grupos de la accion
                List<Data.Entities.QuotesGroups> listToRemove = _dbContext.QuotesGroups.Where(g => g.QuoteId == quoteId).ToList();
                _dbContext.QuotesGroups.RemoveRange(listToRemove);
                //Agregamos los nuevos grupos a la accion
                List<Data.Entities.QuotesGroups> listToAdd = list.Select(g => new Data.Entities.QuotesGroups { QuoteId = quoteId, GroupId = g.ID}).ToList();
                _dbContext.QuotesGroups.AddRange(listToAdd);
                //Guardamos los cambios
                _dbContext.SaveChanges();

                return true;
            } catch
            {
                return false;
            }


        }

    }
}
