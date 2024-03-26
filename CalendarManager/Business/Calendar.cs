using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeAlert.Data.Entities;

namespace CalendarManager.Business
{
    public class Calendar : ICalendar
    {

        private readonly TradeAlertContext _dbContext;

        public Calendar(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<TradeAlertContext>();
        }



        public void Run()
        {
            try
            {
                //Fecha de ejecucucion
                DateTime today = DateTime.Now;
                //hacer un reload de todas las colecciones de la db
                _dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());
                //Agregamos al calendario la presentacion de Balances de las acciones del portfolio
                List<Quotes> quotesPortfolio = _dbContext.Portfolio
                                                         .Include(p => p.quote)
                                                         .Where(p => p.quote.earningsDate != null)
                                                         .Select(p => p.quote)
                                                         .ToList();

                //Eventos de earnings para actualizar o crear
                List<TradeAlert.Data.Entities.Calendar> earningsCalendar = quotesPortfolio.Select(p => new TradeAlert.Data.Entities.Calendar()
                {
                    calendarTypeId = 2,
                    description = $"{p.name} ({p.symbol}) Presenta balance de resultados.",
                    scheduleDate = (DateTime)p.earningsDate,
                    entryDate = today,
                    deleted = false,
                    referenceId = $"2_{p.ID}"
                }).ToList();
                //Actualizamos los earnings
                foreach (TradeAlert.Data.Entities.Calendar cal in earningsCalendar)
                {
                    TradeAlert.Data.Entities.Calendar? calendarToUpdate = _dbContext.Calendar.FirstOrDefault(c => c.referenceId == cal.referenceId);

                    if (calendarToUpdate != null)
                    {
                        //Actualizamos (solo si cambia la fecha del earning)
                        if (calendarToUpdate.scheduleDate != cal.scheduleDate)
                        {
                            calendarToUpdate.scheduleDate = cal.scheduleDate;
                            calendarToUpdate.entryDate = today;
                            _dbContext.Calendar.Update(calendarToUpdate);
                        }
                    }
                    else
                    {
                        //Nuevo evento de earning
                        _dbContext.Calendar.Add(cal);
                    }
                }

                //Eliminamos los earning de las acciones que ya no tenemos
                List<TradeAlert.Data.Entities.Calendar> eventsToDelete = _dbContext.Calendar.Where(c => c.calendarTypeId == 2)
                                                                                            .ToList();
                eventsToDelete = eventsToDelete.Where(c => !earningsCalendar.Any(e => e.referenceId == c.referenceId)).ToList();
                _dbContext.RemoveRange(eventsToDelete);
                
                //Guardar cambios
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
    }
}
