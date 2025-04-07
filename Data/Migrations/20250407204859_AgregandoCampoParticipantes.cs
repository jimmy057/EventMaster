using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMaster.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCampoParticipantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Participantes",
                table: "Actividades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participantes",
                table: "Actividades");
        }
    }
}
