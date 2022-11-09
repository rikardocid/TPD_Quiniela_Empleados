using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class Camposdecontrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Consecutivo",
                table: "codigo_promocional",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "posicion",
                table: "codigo_promocional",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consecutivo",
                table: "codigo_promocional");

            migrationBuilder.DropColumn(
                name: "posicion",
                table: "codigo_promocional");
        }
    }
}
