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

                _dbContext.Quotes
                    .Include(q => q.QuotesAlerts)
                    .ToList()
                    .ForEach(q => q.QuotesAlerts.ToList().ForEach(qa => qa.regularMarketPercentDiff = GetAbsolutePercentDiff(q.regularMarketPrice, qa.price)));

                _dbContext?.SaveChanges();
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
            try
            {
                decimal absDiff = Math.Abs(quotePrice - alertPrice);

                decimal percentDiff = (((quotePrice + absDiff) / quotePrice) - 1) * 100;

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


    }
}
