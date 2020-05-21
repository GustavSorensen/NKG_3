using ngkopgavea.Models;
using ngkopgavea.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngkopgavea
{
    public interface IUnitOfWork : IDisposable
    {
        IWeatherForecastRepository WeatherForecastRepository { get; }
        IUserRepository UserRepository { get; }
        IRepository<Location> LocationRepository { get; }
        public int Commit();
    }
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public DatabaseContext Context { get; protected set; }
        private WeatherForecastRepository weatherForecastRepository;
        private Repository<Location> locationRepository;
        private UserRepository userRepository;
        public UnitOfWork()
        {
            Context = new DatabaseContext();
        }
        public IWeatherForecastRepository WeatherForecastRepository
        {
            get
            {

                if (weatherForecastRepository == null)
                {
                    weatherForecastRepository = new WeatherForecastRepository(Context);
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
        public IUserRepository UserRepository
        {
            get
            {

                if (userRepository == null)
                {
                    userRepository = new UserRepository(Context);
                }
                return userRepository;
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
