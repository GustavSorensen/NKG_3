using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using ngkopgavea;
using ngkopgavea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ngkopgavea.Hubs;

namespace ngkopgavea.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IHubContext<MeasurementHub> hub;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public WeatherForecastsController(IUnitOfWork unit, IHubContext<MeasurementHub> hub)
        {
            this.hub = hub;
            uow = unit;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.Get(), Formatting.Indented, serializerSettings);
                return json;
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("{start}/{end}")]
        public async Task<ActionResult> GetByInterval(DateTime start, DateTime end)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.GetByInterval(start, end), Formatting.Indented, serializerSettings);
                return Ok(json);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("new")]
        public async Task<IActionResult> GetNewest()
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.GetTopThreeNewest(), Formatting.Indented, serializerSettings);
                return Ok(json);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.GetByDate(date), Formatting.Indented, serializerSettings);
                return Ok(json);
            }
            catch
            {
                return NotFound();
            }
        }
        // GET: api/WeatherForecasts/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.Get(id), Formatting.Indented, serializerSettings);
                return Ok(json);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            await uow.WeatherForecastRepository.Add(weatherForecast);
            await hub.Clients.All.SendAsync("NewMeasurements",
                weatherForecast.Date,
                weatherForecast.Location.Name,
                weatherForecast.Location.Latitude,
                weatherForecast.Location.Longitude,
                weatherForecast.TemperatureC,
                weatherForecast.Humidity,
                weatherForecast.AirPressure);
            string json = JsonConvert.SerializeObject(weatherForecast, Formatting.Indented, serializerSettings);
            return Ok(json);
        }
    }
}
