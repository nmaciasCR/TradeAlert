namespace CalendarManager
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Business.ICalendar _businessCalendar;

        public Worker(ILogger<Worker> logger, Business.ICalendar businessCalendar)
        {
            _logger = logger;
            _businessCalendar = businessCalendar;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _businessCalendar.Run();

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60 * 60, stoppingToken); //Cada 1 hora
            }
        }
    }
}