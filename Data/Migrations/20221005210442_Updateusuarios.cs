using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class Updateusuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clave",
                table: "estadioModels",
                newName: "Clave");

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "usuarioModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rol",
                table: "usuarioModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "usuarioModels");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "usuarioModels");

            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "estadioModels",
                newName: "clave");
        }
    }
}
