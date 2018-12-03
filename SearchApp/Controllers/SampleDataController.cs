using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EFGetStarted.AspNetCore.NewDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace SearchApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly MeetingContext _context;

        public SampleDataController(MeetingContext context)
        {
            _context = context;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<Slot> Slots()
        {
            return _context.Slots.ToList();
        }

        [HttpPost("[action]")]
        public int SaveSlot([FromBody] Slot slot)
        {
            slot.SlotDuration = Convert.ToInt16(Regex.Match(slot.SlotDescription, @"\d+").Value);
            if(_context.Slots.Count() == 0)
            {
                slot = InsertFirstSlot(slot);
            }
            else
            {
                slot = InsertSlot(slot);
            }
            _context.Slots.Add(slot);
            return _context.SaveChanges();
        }

        private Slot InsertFirstSlot(Slot slot)
        {
            slot.SlotStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 00, 00);
            slot.SlotEndTime = slot.SlotStartTime.AddMinutes(slot.SlotDuration);
            return slot;
        }

        private Slot InsertSlot(Slot slot)
        {
            slot.SlotStartTime = _context.Slots.Last().SlotEndTime; ;
            slot.SlotEndTime = slot.SlotStartTime.AddMinutes(slot.SlotDuration);
            return slot = AdjustSlot(slot);
        }

        private Slot AdjustSlot(Slot slot)
        {
            DateTime lunchStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 00);
            DateTime lunchEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 00);
            DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 00);
            if (slot.SlotStartTime > lunchStartTime && slot.SlotEndTime < lunchEndTime)
            {
                slot.SlotStartTime = lunchEndTime;
                slot.SlotEndTime = slot.SlotStartTime.AddMinutes(slot.SlotDuration);
            }
            return slot;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
