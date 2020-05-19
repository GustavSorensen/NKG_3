using System;


namespace ngkopgavea.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int Humidity { get; set; }
        public double AirPressure { get; set; }
        public Location Location { get; set; }
    }
}
