using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCore6Identity.Data.Migrations
{
    public partial class CompletoDeTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estadioModels",
                columns: table => new
                {
                    clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadioModels", x => x.clave);
                });

            migrationBuilder.CreateTable(
                name: "generarCodigosPromocionales",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codigos = table.Column<int>(type: "int", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generarCodigosPromocionales", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "usuarioModels",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cabecera = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioModels", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "juegoModels",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaJuego = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaveEstadio = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClaveEquipo1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClaveEquipo2 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_juegoModels", x => x.Clave);
                    table.ForeignKey(
                        name: "FK_juegoModels_equipoModels_ClaveEquipo1",
                        column: x => x.ClaveEquipo1,
                        principalTable: "equipoModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_juegoModels_equipoModels_ClaveEquipo2",
                        column: x => x.ClaveEquipo2,
                        principalTable: "equipoModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_juegoModels_estadioModels_ClaveEstadio",
                        column: x => x.ClaveEstadio,
                        principalTable: "estadioModels",
                        principalColumn: "clave",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "marcardorModels",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaveJuego = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Equipo1 = table.Column<int>(type: "int", nullable: false),
                    Equipo2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcardorModels", x => x.Clave);
                    table.ForeignKey(
                        name: "FK_marcardorModels_juegoModels_ClaveJuego",
                        column: x => x.ClaveJuego,
                        principalTable: "juegoModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quinielaModels",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Prediccion1 = table.Column<int>(type: "int", nullable: false),
                    Prediccion3 = table.Column<int>(type: "int", nullable: false),
                    ClaveJuego = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClaveCodigoPromocional = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quinielaModels", x => x.Clave);
                    table.ForeignKey(
                        name: "FK_quinielaModels_juegoModels_ClaveJuego",
                        column: x => x.ClaveJuego,
                        principalTable: "juegoModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "codigoPromocionalModels",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false),
                    ClaveQuinela = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClaveUsuario = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codigoPromocionalModels", x => x.Clave);
                    table.ForeignKey(
                        name: "FK_codigoPromocionalModels_quinielaModels_ClaveQuinela",
                        column: x => x.ClaveQuinela,
                        principalTable: "quinielaModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_codigoPromocionalModels_usuarioModels_ClaveUsuario",
                        column: x => x.ClaveUsuario,
                        principalTable: "usuarioModels",
                        principalColumn: "Clave",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_codigoPromocionalModels_ClaveQuinela",
                table: "codigoPromocionalModels",
                column: "ClaveQuinela");

            migrationBuilder.CreateIndex(
                name: "IX_codigoPromocionalModels_ClaveUsuario",
                table: "codigoPromocionalModels",
                column: "ClaveUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_juegoModels_ClaveEquipo1",
                table: "juegoModels",
                column: "ClaveEquipo1");

            migrationBuilder.CreateIndex(
                name: "IX_juegoModels_ClaveEquipo2",
                table: "juegoModels",
                column: "ClaveEquipo2");

            migrationBuilder.CreateIndex(
                name: "IX_juegoModels_ClaveEstadio",
                table: "juegoModels",
                column: "ClaveEstadio");

            migrationBuilder.CreateIndex(
                name: "IX_marcardorModels_ClaveJuego",
                table: "marcardorModels",
                column: "ClaveJuego");

            migrationBuilder.CreateIndex(
                name: "IX_quinielaModels_ClaveCodigoPromocional",
                table: "quinielaModels",
                column: "ClaveCodigoPromocional");

            migrationBuilder.CreateIndex(
                name: "IX_quinielaModels_ClaveJuego",
                table: "quinielaModels",
                column: "ClaveJuego");

            migrationBuilder.AddForeignKey(
                name: "FK_quinielaModels_codigoPromocionalModels_ClaveCodigoPromocional",
                table: "quinielaModels",
                column: "ClaveCodigoPromocional",
                principalTable: "codigoPromocionalModels",
                principalColumn: "Clave",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codigoPromocionalModels_quinielaModels_ClaveQuinela",
                table: "codigoPromocionalModels");

            migrationBuilder.DropTable(
                name: "generarCodigosPromocionales");

            migrationBuilder.DropTable(
                name: "marcardorModels");

            migrationBuilder.DropTable(
                name: "quinielaModels");

            migrationBuilder.DropTable(
                name: "codigoPromocionalModels");

            migrationBuilder.DropTable(
                name: "juegoModels");

            migrationBuilder.DropTable(
                name: "usuarioModels");

            migrationBuilder.DropTable(
                name: "estadioModels");
        }
    }
}
