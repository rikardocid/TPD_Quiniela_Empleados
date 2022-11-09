using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class MarcadoresJuegos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaveMarcador",
                table: "juego",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_juego_ClaveMarcador",
                table: "juego",
                column: "ClaveMarcador");

            migrationBuilder.AddForeignKey(
                name: "FK_juego_marcador_ClaveMarcador",
                table: "juego",
                column: "ClaveMarcador",
                principalTable: "marcador",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_juego_marcador_ClaveMarcador",
                table: "juego");

            migrationBuilder.DropIndex(
                name: "IX_juego_ClaveMarcador",
                table: "juego");

            migrationBuilder.DropColumn(
                name: "ClaveMarcador",
                table: "juego");
        }
    }
}
