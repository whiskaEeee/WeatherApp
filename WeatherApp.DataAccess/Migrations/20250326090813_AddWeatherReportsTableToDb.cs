using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherReportsTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Дата = table.Column<DateOnly>(type: "date", nullable: true),
                    Времямосковское = table.Column<TimeOnly>(name: "Время (московское)", type: "time(0)", nullable: true),
                    T = table.Column<double>(type: "float", nullable: true),
                    ОтнВлажностьвоздуха = table.Column<double>(name: "Отн. Влажность воздуха, %", type: "float", nullable: true),
                    Td = table.Column<double>(type: "float", nullable: true),
                    Атмдавлениеммртст = table.Column<int>(name: "Атм. давление, мм рт.ст.", type: "int", nullable: true),
                    Направлениеветра = table.Column<string>(name: "Направление ветра", type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Скоростьветрамс = table.Column<int>(name: "Скорость ветра, м/с", type: "int", nullable: true),
                    Облачность = table.Column<int>(name: "Облачность, %", type: "int", nullable: true),
                    h = table.Column<int>(type: "int", nullable: true),
                    VV = table.Column<int>(type: "int", nullable: true),
                    Погодныеявления = table.Column<string>(name: "Погодные явления", type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherReports");
        }
    }
}
