using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class titulotables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codigoPromocionalModels_quinielaModels_ClaveQuinela",
                table: "codigoPromocionalModels");

            migrationBuilder.DropForeignKey(
                name: "FK_codigoPromocionalModels_usuarioModels_ClaveUsuario",
                table: "codigoPromocionalModels");

            migrationBuilder.DropForeignKey(
                name: "FK_juegoModels_equipoModels_ClaveEquipo1",
                table: "juegoModels");

            migrationBuilder.DropForeignKey(
                name: "FK_juegoModels_equipoModels_ClaveEquipo2",
                table: "juegoModels");

            migrationBuilder.DropForeignKey(
                name: "FK_juegoModels_estadioModels_ClaveEstadio",
                table: "juegoModels");

            migrationBuilder.DropForeignKey(
                name: "FK_marcardorModels_juegoModels_ClaveJuego",
                table: "marcardorModels");

            migrationBuilder.DropForeignKey(
                name: "FK_quinielaModels_codigoPromocionalModels_ClaveCodigoPromocional",
                table: "quinielaModels");

            migrationBuilder.DropForeignKey(
                name: "FK_quinielaModels_juegoModels_ClaveJuego",
                table: "quinielaModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarioModels",
                table: "usuarioModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quinielaModels",
                table: "quinielaModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_marcardorModels",
                table: "marcardorModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_juegoModels",
                table: "juegoModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_generarCodigosPromocionales",
                table: "generarCodigosPromocionales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estadioModels",
                table: "estadioModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_equipoModels",
                table: "equipoModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codigoPromocionalModels",
                table: "codigoPromocionalModels");

            migrationBuilder.RenameTable(
                name: "usuarioModels",
                newName: "usuario");

            migrationBuilder.RenameTable(
                name: "quinielaModels",
                newName: "quiniela");

            migrationBuilder.RenameTable(
                name: "marcardorModels",
                newName: "marcador");

            migrationBuilder.RenameTable(
                name: "juegoModels",
                newName: "juego");

            migrationBuilder.RenameTable(
                name: "generarCodigosPromocionales",
                newName: "generar_codigos_promocionales");

            migrationBuilder.RenameTable(
                name: "estadioModels",
                newName: "estadio");

            migrationBuilder.RenameTable(
                name: "equipoModels",
                newName: "equipo");

            migrationBuilder.RenameTable(
                name: "codigoPromocionalModels",
                newName: "codigo_promocional");

            migrationBuilder.RenameIndex(
                name: "IX_quinielaModels_ClaveJuego",
                table: "quiniela",
                newName: "IX_quiniela_ClaveJuego");

            migrationBuilder.RenameIndex(
                name: "IX_quinielaModels_ClaveCodigoPromocional",
                table: "quiniela",
                newName: "IX_quiniela_ClaveCodigoPromocional");

            migrationBuilder.RenameIndex(
                name: "IX_marcardorModels_ClaveJuego",
                table: "marcador",
                newName: "IX_marcador_ClaveJuego");

            migrationBuilder.RenameIndex(
                name: "IX_juegoModels_ClaveEstadio",
                table: "juego",
                newName: "IX_juego_ClaveEstadio");

            migrationBuilder.RenameIndex(
                name: "IX_juegoModels_ClaveEquipo2",
                table: "juego",
                newName: "IX_juego_ClaveEquipo2");

            migrationBuilder.RenameIndex(
                name: "IX_juegoModels_ClaveEquipo1",
                table: "juego",
                newName: "IX_juego_ClaveEquipo1");

            migrationBuilder.RenameIndex(
                name: "IX_codigoPromocionalModels_ClaveUsuario",
                table: "codigo_promocional",
                newName: "IX_codigo_promocional_ClaveUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_codigoPromocionalModels_ClaveQuinela",
                table: "codigo_promocional",
                newName: "IX_codigo_promocional_ClaveQuinela");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quiniela",
                table: "quiniela",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_marcador",
                table: "marcador",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_juego",
                table: "juego",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_generar_codigos_promocionales",
                table: "generar_codigos_promocionales",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estadio",
                table: "estadio",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipo",
                table: "equipo",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codigo_promocional",
                table: "codigo_promocional",
                column: "Clave");

            migrationBuilder.AddForeignKey(
                name: "FK_codigo_promocional_quiniela_ClaveQuinela",
                table: "codigo_promocional",
                column: "ClaveQuinela",
                principalTable: "quiniela",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_codigo_promocional_usuario_ClaveUsuario",
                table: "codigo_promocional",
                column: "ClaveUsuario",
                principalTable: "usuario",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juego_equipo_ClaveEquipo1",
                table: "juego",
                column: "ClaveEquipo1",
                principalTable: "equipo",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juego_equipo_ClaveEquipo2",
                table: "juego",
                column: "ClaveEquipo2",
                principalTable: "equipo",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juego_estadio_ClaveEstadio",
                table: "juego",
                column: "ClaveEstadio",
                principalTable: "estadio",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_marcador_juego_ClaveJuego",
                table: "marcador",
                column: "ClaveJuego",
                principalTable: "juego",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_quiniela_codigo_promocional_ClaveCodigoPromocional",
                table: "quiniela",
                column: "ClaveCodigoPromocional",
                principalTable: "codigo_promocional",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_quiniela_juego_ClaveJuego",
                table: "quiniela",
                column: "ClaveJuego",
                principalTable: "juego",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codigo_promocional_quiniela_ClaveQuinela",
                table: "codigo_promocional");

            migrationBuilder.DropForeignKey(
                name: "FK_codigo_promocional_usuario_ClaveUsuario",
                table: "codigo_promocional");

            migrationBuilder.DropForeignKey(
                name: "FK_juego_equipo_ClaveEquipo1",
                table: "juego");

            migrationBuilder.DropForeignKey(
                name: "FK_juego_equipo_ClaveEquipo2",
                table: "juego");

            migrationBuilder.DropForeignKey(
                name: "FK_juego_estadio_ClaveEstadio",
                table: "juego");

            migrationBuilder.DropForeignKey(
                name: "FK_marcador_juego_ClaveJuego",
                table: "marcador");

            migrationBuilder.DropForeignKey(
                name: "FK_quiniela_codigo_promocional_ClaveCodigoPromocional",
                table: "quiniela");

            migrationBuilder.DropForeignKey(
                name: "FK_quiniela_juego_ClaveJuego",
                table: "quiniela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quiniela",
                table: "quiniela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_marcador",
                table: "marcador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_juego",
                table: "juego");

            migrationBuilder.DropPrimaryKey(
                name: "PK_generar_codigos_promocionales",
                table: "generar_codigos_promocionales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estadio",
                table: "estadio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_equipo",
                table: "equipo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codigo_promocional",
                table: "codigo_promocional");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "usuarioModels");

            migrationBuilder.RenameTable(
                name: "quiniela",
                newName: "quinielaModels");

            migrationBuilder.RenameTable(
                name: "marcador",
                newName: "marcardorModels");

            migrationBuilder.RenameTable(
                name: "juego",
                newName: "juegoModels");

            migrationBuilder.RenameTable(
                name: "generar_codigos_promocionales",
                newName: "generarCodigosPromocionales");

            migrationBuilder.RenameTable(
                name: "estadio",
                newName: "estadioModels");

            migrationBuilder.RenameTable(
                name: "equipo",
                newName: "equipoModels");

            migrationBuilder.RenameTable(
                name: "codigo_promocional",
                newName: "codigoPromocionalModels");

            migrationBuilder.RenameIndex(
                name: "IX_quiniela_ClaveJuego",
                table: "quinielaModels",
                newName: "IX_quinielaModels_ClaveJuego");

            migrationBuilder.RenameIndex(
                name: "IX_quiniela_ClaveCodigoPromocional",
                table: "quinielaModels",
                newName: "IX_quinielaModels_ClaveCodigoPromocional");

            migrationBuilder.RenameIndex(
                name: "IX_marcador_ClaveJuego",
                table: "marcardorModels",
                newName: "IX_marcardorModels_ClaveJuego");

            migrationBuilder.RenameIndex(
                name: "IX_juego_ClaveEstadio",
                table: "juegoModels",
                newName: "IX_juegoModels_ClaveEstadio");

            migrationBuilder.RenameIndex(
                name: "IX_juego_ClaveEquipo2",
                table: "juegoModels",
                newName: "IX_juegoModels_ClaveEquipo2");

            migrationBuilder.RenameIndex(
                name: "IX_juego_ClaveEquipo1",
                table: "juegoModels",
                newName: "IX_juegoModels_ClaveEquipo1");

            migrationBuilder.RenameIndex(
                name: "IX_codigo_promocional_ClaveUsuario",
                table: "codigoPromocionalModels",
                newName: "IX_codigoPromocionalModels_ClaveUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_codigo_promocional_ClaveQuinela",
                table: "codigoPromocionalModels",
                newName: "IX_codigoPromocionalModels_ClaveQuinela");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarioModels",
                table: "usuarioModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quinielaModels",
                table: "quinielaModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_marcardorModels",
                table: "marcardorModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_juegoModels",
                table: "juegoModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_generarCodigosPromocionales",
                table: "generarCodigosPromocionales",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estadioModels",
                table: "estadioModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipoModels",
                table: "equipoModels",
                column: "Clave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codigoPromocionalModels",
                table: "codigoPromocionalModels",
                column: "Clave");

            migrationBuilder.AddForeignKey(
                name: "FK_codigoPromocionalModels_quinielaModels_ClaveQuinela",
                table: "codigoPromocionalModels",
                column: "ClaveQuinela",
                principalTable: "quinielaModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_codigoPromocionalModels_usuarioModels_ClaveUsuario",
                table: "codigoPromocionalModels",
                column: "ClaveUsuario",
                principalTable: "usuarioModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juegoModels_equipoModels_ClaveEquipo1",
                table: "juegoModels",
                column: "ClaveEquipo1",
                principalTable: "equipoModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juegoModels_equipoModels_ClaveEquipo2",
                table: "juegoModels",
                column: "ClaveEquipo2",
                principalTable: "equipoModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_juegoModels_estadioModels_ClaveEstadio",
                table: "juegoModels",
                column: "ClaveEstadio",
                principalTable: "estadioModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_marcardorModels_juegoModels_ClaveJuego",
                table: "marcardorModels",
                column: "ClaveJuego",
                principalTable: "juegoModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_quinielaModels_codigoPromocionalModels_ClaveCodigoPromocional",
                table: "quinielaModels",
                column: "ClaveCodigoPromocional",
                principalTable: "codigoPromocionalModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_quinielaModels_juegoModels_ClaveJuego",
                table: "quinielaModels",
                column: "ClaveJuego",
                principalTable: "juegoModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
