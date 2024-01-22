using NotificationManager.Business;
using NotificationManager.Business.Interfaces;

namespace NotificationManager
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker>? _logger;
        private readonly IProcessAlerts? _processAlerts;
        private readonly IProcessNotifications? _processNotifications;

        public Worker(ILogger<Worker> logger, IProcessAlerts processAlerts, IProcessNotifications? processNotifications)
        {
            try
            {
                _logger = logger;
                _processAlerts = processAlerts ?? throw new ArgumentNullException(nameof(processAlerts));
                _processNotifications = processNotifications;
            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = colorDefault;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _processAlerts?.Run();
                _processNotifications?.Run();
                _logger?.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60 * 5, stoppingToken); //Cada 5 minutos
            }
        }
    }
}