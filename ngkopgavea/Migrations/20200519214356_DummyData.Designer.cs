﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ngkopgavea;

namespace ngkopgavea.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200519214356_DummyData")]
    partial class DummyData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ngkopgavea.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeatherForecastId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeatherForecastId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ngkopgavea.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ngkopgavea.Models.WeatherForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AirPressure")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecasts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AirPressure = 3.0,
                            Date = new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Humidity = 20,
                            TemperatureC = 1
                        },
                        new
                        {
                            Id = 2,
                            AirPressure = 420.0,
                            Date = new DateTime(2020, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Humidity = 69,
                            TemperatureC = 10
                        },
                        new
                        {
                            Id = 3,
                            AirPressure = 1.0,
                            Date = new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Humidity = 0,
                            TemperatureC = 100
                        });
                });

            modelBuilder.Entity("ngkopgavea.Models.Location", b =>
                {
                    b.HasOne("ngkopgavea.Models.WeatherForecast", "WeatherForecast")
                        .WithOne("Location")
                        .HasForeignKey("ngkopgavea.Models.Location", "WeatherForecastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
