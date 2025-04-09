using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMaster.Migrations
{
    /// <inheritdoc />
    public partial class Otra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "EventosDetalle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Actividades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "EventosDetalle");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Actividades");
        }
    }
}
