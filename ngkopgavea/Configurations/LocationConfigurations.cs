using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ngkopgavea.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ngkopgavea.Configurations
{
    public class LocationConfigurations : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(new Location
            {
                Id = 1,
                WeatherForecastId = 1,
                Latitude = 56.204438,
                Longitude = 10.231398,
                Name = "Gustavs bolig"
            }, new Location
            {
                Id = 2,
                WeatherForecastId = 2,
                Latitude = 69,
                Longitude = 420,
                Name = "Michaels bolig"
            }, new Location
            {
                Id = 3,
                WeatherForecastId = 3,
                Latitude = 1234,
                Longitude = 42,
                Name = "Lucas bolig"
            });
        }
    }
}
