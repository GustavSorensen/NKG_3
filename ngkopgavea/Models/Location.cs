﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ngkopgavea
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
    }
}