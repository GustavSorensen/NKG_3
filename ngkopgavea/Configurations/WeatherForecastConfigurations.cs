﻿using ngkopgavea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ngkopgavea.Configurations
{
    public class WeatherForecastConfigurations : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            
            builder.HasData(new WeatherForecast
            {
                Id = 1,
                Date = new DateTime(2020, 5, 20),
                TemperatureC = 1,
                Humidity = 20,
                AirPressure = 3
            },
            new WeatherForecast
            {
                Id = 2,
                Date = new DateTime(2020, 3, 19),

                TemperatureC = 10,
                Humidity = 69,
                AirPressure = 420
            },
            new WeatherForecast
            {
                Id = 3,
                Date = new DateTime(2019, 5, 2),
                TemperatureC = 100,
                Humidity = 0,
                AirPressure = 1
            });
        }
    }
}
