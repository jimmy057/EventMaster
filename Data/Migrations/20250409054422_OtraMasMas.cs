using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMaster.Migrations
{
    /// <inheritdoc />
    public partial class OtraMasMas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participantes",
                table: "Actividades",
                newName: "Cupos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cupos",
                table: "Actividades",
                newName: "Participantes");
        }
    }
}
