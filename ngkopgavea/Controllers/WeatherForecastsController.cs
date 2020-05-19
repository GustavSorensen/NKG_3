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

namespace ngkopgavea.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public WeatherForecastsController()
        {
            uow = new UnitOfWork();
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
        public async Task<ActionResult<string>> GetByInterval(DateTime start, DateTime end)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.GetByInterval(start, end), Formatting.Indented, serializerSettings);
                return json;
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("new")]
        public async Task<ActionResult<string>> GetNewest()
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
        public async Task<ActionResult<string>> GetByDate(DateTime date)
        {
            try
            {
                string json = JsonConvert.SerializeObject(await uow.WeatherForecastRepository.GetByDate(date), Formatting.Indented, serializerSettings);
                return json;
            }
            catch
            {
                return NotFound();
            }
        }
        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
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
        public async Task<ActionResult<string>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            await uow.WeatherForecastRepository.Add(weatherForecast);
            string json = JsonConvert.SerializeObject(weatherForecast, Formatting.Indented, serializerSettings);
            return json;
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await uow.WeatherForecastRepository.Get(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            await uow.WeatherForecastRepository.Delete(weatherForecast);
            string json = JsonConvert.SerializeObject(weatherForecast, Formatting.Indented, serializerSettings);
            return json;
        }
    }
}
