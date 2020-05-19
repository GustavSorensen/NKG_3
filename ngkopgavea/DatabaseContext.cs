using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ngkopgavea.Models;

namespace ngkopgavea.Models
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(@"Data Source=localhost,1433;Database=vareDatabase;User ID=SA;Password=SecPass1;");
        }
        //Seb: @"Data Source=localhost,1433;Database=vareDatabase;User ID=SA;Password=SecPass1;"
        //Erm: @"Data Source=(localdb)\MSSQLLocalDB;TrustServerCertificate=False;MultiSubnetFailover=False;database=testDB;"
        //Gus: @"Data Source=localhost,1433;Database=vareDatabase;User ID=SA;Password=Password1!;"

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    } 
}
