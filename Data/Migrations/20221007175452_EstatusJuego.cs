using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class EstatusJuego : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "juego",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "juego");
        }
    }
}
