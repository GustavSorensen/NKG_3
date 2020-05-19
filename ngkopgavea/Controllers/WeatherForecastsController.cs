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

namespace ngkopgavea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public WeatherForecastsController()
        {
            uow = new UnitOfWork();
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<string>> GetWeatherForecasts()
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

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetWeatherForecast(int id)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.Get(id), Formatting.Indented, serializerSettings);
                return json;
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
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            await uow.WeatherForecastRepository.Add(weatherForecast);
            return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherForecast>> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await uow.WeatherForecastRepository.Get(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            await uow.WeatherForecastRepository.Delete(weatherForecast);
            return weatherForecast;
        }
    }
}
