using Microsoft.EntityFrameworkCore.Migrations;

namespace ngkopgavea.Migrations
{
    public partial class LocationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "WeatherForecastId" },
                values: new object[] { 1, 56.204438000000003, 10.231398, "Gustavs bolig", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "WeatherForecastId" },
                values: new object[] { 2, 69.0, 420.0, "Michaels bolig", 2 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "WeatherForecastId" },
                values: new object[] { 3, 1234.0, 42.0, "Lucas bolig", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
