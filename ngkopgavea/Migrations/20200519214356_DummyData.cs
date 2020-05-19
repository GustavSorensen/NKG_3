using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ngkopgavea.Migrations
{
    public partial class DummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "AirPressure", "Date", "Humidity", "TemperatureC" },
                values: new object[] { 1, 3.0, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 1 });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "AirPressure", "Date", "Humidity", "TemperatureC" },
                values: new object[] { 2, 420.0, new DateTime(2020, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 69, 10 });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "AirPressure", "Date", "Humidity", "TemperatureC" },
                values: new object[] { 3, 1.0, new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
