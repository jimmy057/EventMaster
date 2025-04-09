using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMaster.Migrations
{
    /// <inheritdoc />
    public partial class OtraMas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "EventosDetalle");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "EventosDetalle");

            migrationBuilder.DropColumn(
                name: "Cupos",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Actividades");

            migrationBuilder.AddColumn<int>(
                name: "Participantes",
                table: "EventosDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFin",
                table: "Actividades",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraInicio",
                table: "Actividades",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participantes",
                table: "EventosDetalle");

            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Actividades");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFin",
                table: "EventosDetalle",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraInicio",
                table: "EventosDetalle",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Cupos",
                table: "Eventos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Actividades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
