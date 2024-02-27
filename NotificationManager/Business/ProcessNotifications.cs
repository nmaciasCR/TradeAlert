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
        int EARNINGS_3DAYS_TYPE = 4;
        int EARNINGS_TODAY_TYPE = 5;

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
                //Generamos las notificaciones de Earnings
                Earnings();
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



        private void Earnings()
        {
            List<Notifications> list = new List<Notifications>();
            DateTime today = DateTime.Now.Date;
            try
            {
                //acciones del portafolio a evaluar
                List<Portfolio> portfolioQuotes = _dbContext.Portfolio
                                                            .Include(p => p.quote)
                                                            .Include(p => p.quote.market)
                                                            .Where(p => p.quote.earningsDate != null)
                                                            .ToList();

                //Verificamos si presentar earning hoy o dentro de 3 dias
                foreach (Portfolio portfolio in portfolioQuotes)
                {
                    //Hoy presenta resultados?
                    if (portfolio.quote.earningsDate >= today && portfolio.quote.earningsDate < today.AddDays(1))
                    {
                        list.Add(new Notifications
                        {
                            notificationTypeId = EARNINGS_TODAY_TYPE,
                            entryDate = DateTime.Now,
                            title = portfolio.quote.symbol,
                            description = $"Hoy presentación de resultados. El {portfolio.quote.earningsDate?.ToString("dd-MM-yyyy hh:mm:ss")} (Hora de {portfolio.quote.market.description})",
                            referenceId = $"{portfolio.quote.ID}",
                            active = true,
                            deleted = false
                        });
                    }
                    else if (portfolio.quote.earningsDate >= today.AddDays(3) && portfolio.quote.earningsDate < today.AddDays(4))
                    {
                        //Presenta balance dentro de 3 dias?
                        list.Add(new Notifications
                        {
                            notificationTypeId = EARNINGS_3DAYS_TYPE,
                            entryDate = DateTime.Now,
                            title = portfolio.quote.symbol,
                            description = $"Presentará resultados dentro de 3 dias. El {portfolio.quote.earningsDate?.ToString("dd-MM-yyyy hh:mm:ss")} (Hora de {portfolio.quote.market.description})",
                            referenceId = $"{portfolio.quote.ID}",
                            active = true,
                            deleted = false
                        });

                    }
                }

                //Eliminamos en caso de estar repetida
                list.RemoveAll(en => _dbContext.Notifications.ToList().Exists(noti => noti.notificationTypeId == en.notificationTypeId
                                                                                        && noti.referenceId == en.referenceId
                                                                                        && noti.description == en.description));
                //Guardamos las nuevas notificaciones
                _dbContext.Notifications.AddRange(list);
                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                ConsoleColor colorDefault = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocurrio un error al crear la notificacion de EARNINGS. Exception: " + ex.Message);
                Console.ForegroundColor = colorDefault;
            }
        }

    }
}
