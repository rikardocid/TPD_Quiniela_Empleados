using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class Cantidadfaltante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CantidadFaltante",
                table: "generar_codigos_promocionales",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadFaltante",
                table: "generar_codigos_promocionales");
        }
    }
}
