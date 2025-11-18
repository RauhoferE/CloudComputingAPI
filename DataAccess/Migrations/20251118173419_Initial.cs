using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperatureCelsius = table.Column<float>(type: "real", nullable: false),
                    HumidityPercent = table.Column<float>(type: "real", nullable: false),
                    WindSpeedKph = table.Column<float>(type: "real", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherDatas_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherDatas_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Conditions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Sunny weather", "Sunny" },
                    { 2, "Cloudy weather", "Cloudy" },
                    { 3, "Rainy weather", "Rainy" },
                    { 4, "Stormy weather", "Stormy" },
                    { 5, "Snowy weather", "Snowy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "US-East" },
                    { 2, "US-West" },
                    { 3, "Europe" },
                    { 4, "Asia" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, "New York", 1 },
                    { 2, "Philadelphia", 1 },
                    { 3, "Washington D.C.", 1 },
                    { 4, "Los Angeles", 2 },
                    { 5, "San Francisco", 2 },
                    { 6, "Las Vegas", 2 },
                    { 7, "London", 3 },
                    { 8, "Paris", 3 },
                    { 9, "Vienna", 3 },
                    { 10, "Tokyo", 4 },
                    { 11, "Osaka", 4 },
                    { 12, "Busan", 4 }
                });

            migrationBuilder.InsertData(
                table: "WeatherDatas",
                columns: new[] { "Id", "CityId", "ConditionId", "Date", "HumidityPercent", "TemperatureCelsius", "WindSpeedKph" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 7, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 65f, 28.5f, 12.5f },
                    { 2, 1, 3, new DateTime(2025, 4, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), 88f, 15.2f, 8f },
                    { 3, 1, 5, new DateTime(2025, 1, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), 75f, -1f, 20f },
                    { 4, 2, 1, new DateTime(2025, 8, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), 60f, 31f, 15f },
                    { 5, 2, 2, new DateTime(2025, 5, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 70f, 18.5f, 10f },
                    { 6, 2, 4, new DateTime(2025, 9, 25, 17, 30, 0, 0, DateTimeKind.Unspecified), 90f, 16f, 25f },
                    { 7, 3, 1, new DateTime(2025, 6, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 55f, 26f, 11f },
                    { 8, 3, 2, new DateTime(2025, 12, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 80f, 4.5f, 5f },
                    { 9, 3, 3, new DateTime(2025, 3, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 92f, 8.2f, 18f },
                    { 10, 4, 1, new DateTime(2025, 9, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), 35f, 32f, 5f },
                    { 11, 4, 2, new DateTime(2025, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 50f, 21f, 10f },
                    { 12, 4, 3, new DateTime(2025, 2, 28, 16, 0, 0, 0, DateTimeKind.Unspecified), 75f, 17.5f, 8.5f },
                    { 13, 5, 2, new DateTime(2025, 7, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 78f, 19.5f, 30f },
                    { 14, 5, 1, new DateTime(2025, 10, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), 60f, 24f, 15f },
                    { 15, 5, 3, new DateTime(2025, 3, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), 85f, 13f, 18f },
                    { 16, 6, 1, new DateTime(2025, 8, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), 15f, 40.5f, 10f },
                    { 17, 6, 1, new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 30f, 14f, 5f },
                    { 18, 6, 2, new DateTime(2025, 12, 24, 8, 0, 0, 0, DateTimeKind.Unspecified), 45f, 8.8f, 12f },
                    { 19, 7, 2, new DateTime(2025, 6, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 75f, 18f, 10f },
                    { 20, 7, 3, new DateTime(2025, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 90f, 12.5f, 15f },
                    { 21, 7, 1, new DateTime(2025, 7, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), 55f, 25.5f, 8f },
                    { 22, 8, 1, new DateTime(2025, 8, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 50f, 30f, 10f },
                    { 23, 8, 3, new DateTime(2025, 4, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 65f, 14f, 18f },
                    { 24, 8, 2, new DateTime(2025, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), 75f, 9.5f, 5f },
                    { 25, 9, 1, new DateTime(2025, 9, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 60f, 22f, 15f },
                    { 26, 9, 5, new DateTime(2025, 2, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), 80f, 2f, 5f },
                    { 27, 9, 4, new DateTime(2025, 7, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 45f, 29.5f, 25f },
                    { 28, 10, 3, new DateTime(2025, 8, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 85f, 33f, 10f },
                    { 29, 10, 1, new DateTime(2025, 11, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 60f, 17.5f, 12f },
                    { 30, 10, 2, new DateTime(2025, 1, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 70f, 6f, 5f },
                    { 31, 11, 4, new DateTime(2025, 7, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), 90f, 30.5f, 20f },
                    { 32, 11, 2, new DateTime(2025, 4, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 75f, 15f, 8f },
                    { 33, 11, 1, new DateTime(2025, 12, 18, 13, 0, 0, 0, DateTimeKind.Unspecified), 65f, 8f, 10f },
                    { 34, 12, 1, new DateTime(2025, 6, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), 70f, 25f, 18f },
                    { 35, 12, 3, new DateTime(2025, 10, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), 80f, 17f, 5f },
                    { 36, 12, 2, new DateTime(2025, 3, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 72f, 9.5f, 14f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Name",
                table: "Conditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherDatas_CityId_Date",
                table: "WeatherDatas",
                columns: new[] { "CityId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherDatas_ConditionId",
                table: "WeatherDatas",
                column: "ConditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherDatas");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
