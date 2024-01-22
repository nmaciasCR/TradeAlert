using Microsoft.EntityFrameworkCore;
using NotificationManager.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.Entities;

namespace NotificationManager.Business
{
    public class ProcessNotifications : IProcessNotifications
    {
        TradeAlertContext _dbContext;
        int SUPPORT_TYPE = 1;
        int RESISTOR_TYPE = 2;
        int CALENDAR_TYPE = 3;

        public ProcessNotifications(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<TradeAlertContext>();
        }

        /// <summary>
        /// Genera las notificaciones correspondientes
        /// para acciones del portfolio e indicadores proncipales
        /// (las que se muestran en el header)
        /// </summary>
        public void Run()
        {
            try
            {
                //hacer un reload de todas las colecciones de la db
                _dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                //Generamos las notificaciones con las alertas de las acciones
                QuotesAlerts();

            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = colorDefault;
            }
        }

        /// <summary>
        /// Genera las notificaciones segun las alertas de las acciones
        /// </summary>
        private void QuotesAlerts()
        {
            List<QuotesAlerts> alertsToProcess;
            List<Notifications> supportNotifications = new List<Notifications>();

            try
            {
                //acciones a evaluar
                alertsToProcess = _dbContext.QuotesAlerts
                                            .Include(q => q.Quote)
                                            .Include(q => q.Quote.Portfolio)
                                            .Include(q => q.Quote.currency)
                                            .Where(q => q.Quote.Portfolio != null || q.Quote.isMainIndex)
                                            .ToList();

                //Generamos notificaciones de SOPORTE
                //cuando haya una alerta de tipo soporte mayor a la cotizacion de la accion
                supportNotifications.AddRange(GetQuotesAlertSupportNotifications(alertsToProcess.Where(a => a.QuoteAlertTypeId == 1).ToList()));
                //Generamos notificaciones de RESISTENCIA
                //cuando haya una alerta de tipo resistencia menor a la cotizacion de la accion
                supportNotifications.AddRange(GetQuotesAlertResistorNotifications(alertsToProcess.Where(a => a.QuoteAlertTypeId == 2).ToList()));

                //Eliminamos las notificaciones creadas anteriormente
                supportNotifications.RemoveAll(sn => _dbContext.Notifications.ToList().Exists(n => n.referenceId == sn.referenceId));
                //agregamos las nuevas notificaciones
                _dbContext.Notifications.AddRange(supportNotifications);
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

        /// <summary>
        /// Retorna un listado de Notificaciones generadas por las alertas de tipo SOPORTE
        /// </summary>
        /// <param name="alertList"></param>
        /// <returns></returns>
        private List<Notifications> GetQuotesAlertSupportNotifications(List<QuotesAlerts> alertList)
        {
            try
            {
                return alertList.Where(a => a.price > a.Quote.regularMarketPrice)
                                .Select(alert => new Notifications
                                {
                                    notificationTypeId = SUPPORT_TYPE,
                                    entryDate = DateTime.Now,
                                    title = alert.Quote.symbol,
                                    description = $"Ha perforado el soporte de {alert.price} {alert.Quote.currency.code}",
                                    referenceId = $"{alert.Quote.ID}_{alert.ID}",
                                    active = true,
                                    deleted = false
                                })
                                .ToList();

            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = colorDefault;

                return new List<Notifications>();
            }
        }

        /// <summary>
        /// Retorna un listado de Notificaciones generadas por las alertas de tipo RESISTENCIA
        /// </summary>
        /// <param name="alertList"></param>
        /// <returns></returns>
        private List<Notifications> GetQuotesAlertResistorNotifications(List<QuotesAlerts> alertList)
        {
            try
            {
                return alertList.Where(a => a.price < a.Quote.regularMarketPrice)
                                .Select(alert => new Notifications
                                {
                                    notificationTypeId = RESISTOR_TYPE,
                                    entryDate = DateTime.Now,
                                    title = alert.Quote.symbol,
                                    description = $"Ha superado la resistencia de {alert.price} {alert.Quote.currency.code}",
                                    referenceId = $"{alert.Quote.ID}_{alert.ID}",
                                    active = true,
                                    deleted = false
                                })
                                .ToList();

            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = colorDefault;

                return new List<Notifications>();
            }
        }


    }
}
