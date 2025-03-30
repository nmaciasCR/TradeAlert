using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Interfaces;
using TradeAlert.Data.DTO;

namespace TradeAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {

        private readonly ICalendar _businessCalendar;

        public CalendarController(ICalendar businessCalendar)
        {
            _businessCalendar = businessCalendar;
        }

        [HttpGet]
        [Route("GetCurrentList")]
        public IActionResult GetCurrentList()
        {
            Dictionary<DateTime, List<CalendarDTO>> calendarDictionary;

            try
            {
                calendarDictionary = _businessCalendar.GetCurrentList()
                                                      .GroupBy(c => c.scheduleDate.Date)
                                                      .OrderBy(d => d.Key)
                                                      .ToDictionary(d => d.Key, d => d.Select(c => _businessCalendar.MapToDTO(c))
                                                                                      .ToList());

                return StatusCode(StatusCodes.Status200OK, calendarDictionary);

            } catch {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
