using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NUnit.Framework;
using NSubstitute;
using ngkopgavea.Controllers;
using ngkopgavea.Models;
using ngkopgavea.Hubs;
using ngkopgavea.RepositoryPattern;
using System.Text.Json;
using ngkopgavea;

namespace NGK3_NUnitTest
{
    [TestFixture]
    class WeatherForecastsUnitTest
    {
        private IHubContext<MeasurementHub> _hubContext;
        private WeatherForecastsController _uut;
        private IUnitOfWork _unit;
        [SetUp]
        public void Setup()
        {
            _hubContext = Substitute.For<IHubContext<MeasurementHub>>();
            _unit = Substitute.For<IUnitOfWork>();
            _uut = new WeatherForecastsController(_unit ,_hubContext);
        }


        [Test]
        public async Task GetLastThreeWeatherObservations()
        {
            //Arrange

            //Act
            var result = (await _uut.GetNewest()) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            await _unit.WeatherForecastRepository.Received().GetTopThreeNewest();
        }

        [Test]
        public async Task GetWeatherObservationByDate()
        {
            //Arrange
            WeatherForecast weatherObservations = new WeatherForecast()
            {

                Date = new DateTime(2020, 6, 20),
                Location = new Location()
                {
                    Name = "Vejle",
                    Latitude = 1013031,
                    Longitude = 2554322
                },
                TemperatureC = 11,
                Humidity = 14,
                AirPressure = 5
            };
            DateTime testDate = new DateTime(2020, 5, 20);
            //Act
            var result = (await _uut.GetByDate(testDate)) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            await _unit.WeatherForecastRepository.Received().GetByDate(testDate);

        }

        [Test]
        public async Task GetWeatherObservationsBetweenDates()
        {
            //Arrange
            WeatherForecast weatherObservations = new WeatherForecast()
            {

                Date = new DateTime(2020, 7, 15),
                Location = new Location()
                {
                    Name = "Vordingborg",
                    Latitude = 450,
                    Longitude = 255
                },
                TemperatureC = 1,
                Humidity = 4,
                AirPressure = 1
            };
            DateTime startDate = new DateTime(2020, 6, 28);
            DateTime endDate = new DateTime(2020, 6, 10);
            //Act
            var result = (await _uut.GetByInterval(startDate, endDate)) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            await _unit.WeatherForecastRepository.Received().GetByInterval(startDate, endDate);
        }
    }
}