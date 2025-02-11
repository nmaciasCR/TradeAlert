using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TradeAlert.Business
{
    public class Markets : Interfaces.IMarkets
    {

        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;



        public Markets(Data.Entities.TradeAlertContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;

        }

        /// <summary>
        /// Retorna un objeto Market por ID
        /// </summary>
        /// <param name="id"></param>
        public Data.Entities.Markets Get(int id)
        {
            return _dbContext.Markets.Find(id);
        }

        /// <summary>
        /// Retorna un listado de todos los mercados
        /// </summary>
        /// <returns></returns>
        public IQueryable<Data.Entities.Markets> GetList()
        {
            return _dbContext.Markets
                            .AsNoTracking()
                            .Include(m => m.Quotes);
        }

        public DTO.MarketDTO MapToDTO(Data.Entities.Markets market)
        {
            DTO.MarketDTO dtoReturn = _mapper.Map<DTO.MarketDTO>(market);

            return dtoReturn;
        }



    }
}
