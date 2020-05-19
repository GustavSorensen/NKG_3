using ngkopgavea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngkopgavea
{
    public class UnitOfWork : IDisposable
    {
        public DatabaseContext Context { get; protected set; }
        private Repository<WeatherForecast> weatherForecastRepository;
        private Repository<Location> locationRepository;
        public UnitOfWork()
        {
            Context = new DatabaseContext();
        }
        public IRepository<WeatherForecast> WeatherForecastRepository
        {
            get
            {

                if (this.weatherForecastRepository == null)
                {
                    this.weatherForecastRepository = new Repository<WeatherForecast>(Context);
                }
                return weatherForecastRepository;
            }
        }
        public IRepository<Location> LocationRepository
        {
            get
            {

                if (locationRepository == null)
                {
                    locationRepository = new Repository<Location>(Context);
                }
                return locationRepository;
            }
        }
        public int Commit()
        {
            return Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
