using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeAlert.Data.DTO;
using TradeAlert.Data.Entities;
using TradeAlert.MemoryCache.Business;
using TradeAlert.MemoryCache.Interfaces;

namespace TradeAlert.MemoryCache
{
    public class MemoryCacheService : IMemoryCacheService
    {

        private readonly IMemoryCache _cache;
        private readonly IStocks _bsStocks;
        private readonly IMarkets _bsMarkets;
        private readonly IPortfolio _bsPortfolio;
        private readonly ICurrencies _bsCurrencies;
        private readonly IQuotesAlerts _bsQuotesAlerts;

        const string stocksKey = "stocks";


        public MemoryCacheService(IMemoryCache cache, IStocks bsStocks, IMarkets bsMarkets, IPortfolio bsPortfolio, ICurrencies bsCurrencies, IQuotesAlerts bsQuotesAlert)
        {
            _cache = cache;
            _bsStocks = bsStocks;
            _bsMarkets = bsMarkets;
            _bsPortfolio = bsPortfolio;
            _bsCurrencies = bsCurrencies;
            _bsQuotesAlerts = bsQuotesAlert;
        }



        public Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            //Para medir tiempo de ejecucion
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var jsonData = JsonSerializer.Serialize(value);
            _cache.Set(key, jsonData, duration);

            //Tiempo de ejecucion
            stopwatch.Stop();
            Console.WriteLine($"Tiempo de ejecución de SetAsync(): {stopwatch.ElapsedMilliseconds} ms.");

            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            //Para medir tiempo de ejecucion
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            if (_cache.TryGetValue(key, out string jsonData))
            {
                return Task.FromResult(JsonSerializer.Deserialize<T>(jsonData));
            }

            //Tiempo de ejecucion
            stopwatch.Stop();
            Console.WriteLine($"Tiempo de ejecución de GetAsync(): {stopwatch.ElapsedMilliseconds} ms.");

            return Task.FromResult<T?>(default);
        }


        public async Task<int> RefreshStocksAllCache()
        {
            try
            {
                //Para medir tiempo de ejecucion
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();


                IQueryable<Data.Entities.Quotes> stocks = _bsStocks.GetList();

                //Mapeamos los stocks para cachear
                List<StocksDTO> quotesDTOList = stocks
                                                .ToList()
                                                .Select(q =>
                                                {
                                                    StocksDTO stocksDTO = _bsStocks.MapToDTO(q);
                                                    stocksDTO._market = _bsMarkets.MapToDTO(q.market);
                                                    if (q.Portfolio != null)
                                                    {
                                                        stocksDTO._Portfolio = _bsPortfolio.MapToDTO(q.Portfolio);
                                                        stocksDTO._Portfolio.euroProfit = stocksDTO._Portfolio.profit * q.currency.euroExchange;
                                                    }
                                                    stocksDTO._currency = _bsCurrencies.MapToDTO(q.currency);
                                                    stocksDTO._alerts = _bsQuotesAlerts.MapToDTO(q.QuotesAlerts.ToList());
                                                    stocksDTO.groupsIdList = q.QuotesGroups.Select(g => g.GroupId).ToList();
                                                    return stocksDTO;
                                                }).ToList();

                //cacheamos
                await SetAsync(stocksKey, quotesDTOList, TimeSpan.FromDays(1));

                //Tiempo de ejecucion
                stopwatch.Stop();
                Console.WriteLine($"Tiempo de ejecución de RefreshStocksAllCache(): {stopwatch.ElapsedMilliseconds} ms.");


                return quotesDTOList.Count;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Actualiza un elemento en la coleccion
        /// </summary>
        public async Task<bool> UpdateStock(int quoteId)
        {
            //Para medir tiempo de ejecucion
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Quotes updateDTO = _bsStocks.Get(quoteId);

            StocksDTO stocksDTO = _bsStocks.MapToDTO(updateDTO);
            stocksDTO._market = _bsMarkets.MapToDTO(updateDTO.market);
            if (updateDTO.Portfolio != null)
            {
                stocksDTO._Portfolio = _bsPortfolio.MapToDTO(updateDTO.Portfolio);
                stocksDTO._Portfolio.euroProfit = stocksDTO._Portfolio.profit * updateDTO.currency.euroExchange;
            }
            stocksDTO._currency = _bsCurrencies.MapToDTO(updateDTO.currency);
            stocksDTO._alerts = _bsQuotesAlerts.MapToDTO(updateDTO.QuotesAlerts.ToList());
            stocksDTO.groupsIdList = updateDTO.QuotesGroups.Select(g => g.GroupId).ToList();

            //Obtener la colección completa desde la caché
            var stocks = await GetAsync<List<StocksDTO>>(stocksKey);

            if (stocks == null || !stocks.Any()) return false; //No hay datos en caché

            //Buscar el elemento a actualizar
            var index = stocks.FindIndex(s => s.symbol == updateDTO.symbol);
            if (index == -1) return false; // No encontrado

            //Reemplazar el elemento en la lista
            stocks[index] = stocksDTO;

            //Guardar la lista actualizada en caché
            await SetAsync(stocksKey, stocks, TimeSpan.FromDays(1));

            //Tiempo de ejecucion
            stopwatch.Stop();
            Console.WriteLine($"Tiempo de ejecución de UpdateStock(Quotes): {stopwatch.ElapsedMilliseconds} ms.");

            return true; // Elemento actualizado correctamente
        }

    }
}
