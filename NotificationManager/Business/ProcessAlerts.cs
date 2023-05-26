using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TradeAlert.Data.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace NotificationManager.Business
{
    public class ProcessAlerts : Interfaces.IProcessAlerts
    {
        TradeAlertContext _dbContext;

        public ProcessAlerts(IServiceProvider serviceProvider)
        {
            //using (var scope = serviceProvider.CreateScope())
            //{
            //    _dbContext = scope.ServiceProvider.GetRequiredService<TradeAlert.Data.Entities.TradeAlertContext>();
            //}

            var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<TradeAlertContext>();
        }


        public void Run()
        {
            try
            {
                //hacer un reload de todas las colecciones de la db
                _dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                //Setea el porcentaje de diferencia entre el valor de la alerta y la cotizacion de la accion
                _dbContext.Quotes
                    .Include(q => q.QuotesAlerts)
                    .ToList()
                    .ForEach(q => q.QuotesAlerts.ToList().ForEach(qa => qa.regularMarketPercentDiff = GetAbsolutePercentDiff(q.regularMarketPrice, qa.price)));

                //Define la prioridad de la accion
                _dbContext.Quotes
                    .ToList()
                    .ForEach(q => q.priorityId = GetStockPriority(q.QuotesAlerts.Select(qa => qa.regularMarketPercentDiff).ToList()));

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = colorDefault;

            }


        }


        private decimal GetAbsolutePercentDiff(decimal quotePrice, decimal alertPrice)
        {
            decimal percentDiff = 0;
            try
            {
                if (quotePrice > 0)
                {
                    decimal absDiff = Math.Abs(quotePrice - alertPrice);
                    percentDiff = (((quotePrice + absDiff) / quotePrice) - 1) * 100;
                }

                return percentDiff;

            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"quotePrice: {quotePrice}, alertPrice: {alertPrice}: {ex.Message}");
                Console.ForegroundColor = colorDefault;

                return 0;
            }

        }

        /// <summary>
        /// Retorna la prioridad de una accion
        /// </summary>
        private int GetStockPriority(List<decimal> priceAlertsDiff)
        {
            int priorityId;

            if (priceAlertsDiff.Any(p => p <= 3))
            {
                priorityId = 1;
            }
            else if (priceAlertsDiff.Any(p => p <= 7))
            {
                priorityId = 2;
            }
            else
            {
                priorityId = 3;
            }

            return priorityId;
        }




    }
}
