﻿using Microsoft.EntityFrameworkCore;
using ngkopgavea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngkopgavea.RepositoryPattern
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecast>
    {
        Task<List<WeatherForecast>> GetTopThreeNewest();
        Task<List<WeatherForecast>> GetByDate(DateTime date);
        Task<List<WeatherForecast>> GetByInterval(DateTime startDate, DateTime endDate);
    }
    public class WeatherForecastRepository : Repository<WeatherForecast>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(DatabaseContext context) : base(context)
        {

        }
        public async Task<List<WeatherForecast>> GetTopThreeNewest()
        {
            return await Context.WeatherForecasts.OrderBy(x => x.Date).Take(3).Include(x => x.Location).ToListAsync();
        }
        public async Task<List<WeatherForecast>> GetByDate(DateTime date)
        {
            return await Context.WeatherForecasts.Where(x => x.Date == date).Include(x => x.Location).ToListAsync();
        }
        public async Task<List<WeatherForecast>> GetByInterval(DateTime startDate, DateTime endDate)
        {
            return await Context.WeatherForecasts.Where(x => x.Date >= startDate && x.Date <= endDate).Include(x => x.Location).ToListAsync();
        }
    }
}
