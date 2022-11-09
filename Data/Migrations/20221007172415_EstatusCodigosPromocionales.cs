using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class EstatusCodigosPromocionales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Puntaje",
                table: "quiniela",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Utilizado",
                table: "codigo_promocional",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Puntaje",
                table: "quiniela");

            migrationBuilder.AlterColumn<bool>(
                name: "Utilizado",
                table: "codigo_promocional",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
