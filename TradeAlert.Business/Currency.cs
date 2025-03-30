using AutoMapper;
using System.Linq;

namespace TradeAlert.Business
{
    public class Currency : Interfaces.ICurrency
    {
        private readonly IMapper _mapper;
        private Data.Entities.TradeAlertContext _dbContext;

        public Currency(IMapper mapper, Data.Entities.TradeAlertContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retorna un objeto currency
        /// </summary>
        public Data.Entities.Currencies Get(int id)
        {
            return _dbContext.Currencies.Find(id);
        }

        /// <summary>
        /// Mapea un objeto currency en su correspondiente DTO
        /// </summary>
        public Data.DTO.CurrencyDTO MapToDTO(Data.Entities.Currencies currency)
        {
            Data.DTO.CurrencyDTO DTOReturn = _mapper.Map<Data.DTO.CurrencyDTO>(currency);
            return DTOReturn;
        }

        /// <summary>
        /// Convierte un monto a euros
        /// </summary>
        public double ConvertToEuro(double amount, Data.Entities.Currencies currencyFrom)
        {
            double tempAmount = amount;

            if (currencyFrom.ID == Data.Static.Currency.LIBRA_ESTERLINA)
            {
                tempAmount = tempAmount / 100;
            }

            return tempAmount * currencyFrom.euroExchange;
        }


    }
}
