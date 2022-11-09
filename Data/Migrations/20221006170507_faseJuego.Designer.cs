﻿// <auto-generated />
using System;
using ASPNetCore6Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASPNetCore6Identity.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221006170507_faseJuego")]
    partial class faseJuego
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPNetCore6Identity.Models.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("apellido_materno");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Apellido_paterno");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("domicilio");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Estatus")
                        .HasColumnType("int")
                        .HasColumnName("estatus");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nombre");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Quinela_TPD.Models.CodigoPromocionalModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaveQuinela")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveQuinela");

                    b.Property<string>("ClaveUsuario")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveUsuario");

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Utilizado")
                        .HasColumnType("bit");

                    b.HasKey("Clave");

                    b.HasIndex("ClaveQuinela");

                    b.HasIndex("ClaveUsuario");

                    b.ToTable("codigoPromocionalModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.EquipoModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bandera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grupo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Clave");

                    b.ToTable("equipoModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.EstadioModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Clave");

                    b.ToTable("estadioModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.GenerarCodigosPromocionalesModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Codigos")
                        .HasColumnType("int");

                    b.Property<int>("CodigosGenerados")
                        .HasColumnType("int");

                    b.Property<string>("EmailCliente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Clave");

                    b.ToTable("generarCodigosPromocionales");
                });

            modelBuilder.Entity("Quinela_TPD.Models.JuegoModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaveEquipo1")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveEquipo1");

                    b.Property<string>("ClaveEquipo2")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveEquipo2");

                    b.Property<string>("ClaveEstadio")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveEstadio");

                    b.Property<int>("Fase")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaJuego")
                        .HasColumnType("datetime2");

                    b.HasKey("Clave");

                    b.HasIndex("ClaveEquipo1");

                    b.HasIndex("ClaveEquipo2");

                    b.HasIndex("ClaveEstadio");

                    b.ToTable("juegoModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.MarcardorModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaveJuego")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveJuego");

                    b.Property<int>("Equipo1")
                        .HasColumnType("int");

                    b.Property<int>("Equipo2")
                        .HasColumnType("int");

                    b.HasKey("Clave");

                    b.HasIndex("ClaveJuego");

                    b.ToTable("marcardorModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.QuinielaModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaveCodigoPromocional")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveCodigoPromocional");

                    b.Property<string>("ClaveJuego")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ClaveJuego");

                    b.Property<int>("Prediccion1")
                        .HasColumnType("int");

                    b.Property<int>("Prediccion3")
                        .HasColumnType("int");

                    b.HasKey("Clave");

                    b.HasIndex("ClaveCodigoPromocional");

                    b.HasIndex("ClaveJuego");

                    b.ToTable("quinielaModels");
                });

            modelBuilder.Entity("Quinela_TPD.Models.UsuarioModel", b =>
                {
                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Cabecera")
                        .HasColumnType("bit");

                    b.Property<int>("Estatus")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Clave");

                    b.ToTable("usuarioModels");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ASPNetCore6Identity.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ASPNetCore6Identity.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASPNetCore6Identity.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ASPNetCore6Identity.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quinela_TPD.Models.CodigoPromocionalModel", b =>
                {
                    b.HasOne("Quinela_TPD.Models.QuinielaModel", "QuinielaModel")
                        .WithMany()
                        .HasForeignKey("ClaveQuinela");

                    b.HasOne("Quinela_TPD.Models.UsuarioModel", "UsuarioModel")
                        .WithMany()
                        .HasForeignKey("ClaveUsuario");

                    b.Navigation("QuinielaModel");

                    b.Navigation("UsuarioModel");
                });

            modelBuilder.Entity("Quinela_TPD.Models.JuegoModel", b =>
                {
                    b.HasOne("Quinela_TPD.Models.EquipoModel", "EquipoModel1")
                        .WithMany()
                        .HasForeignKey("ClaveEquipo1");

                    b.HasOne("Quinela_TPD.Models.EquipoModel", "EquipoModel2")
                        .WithMany()
                        .HasForeignKey("ClaveEquipo2");

                    b.HasOne("Quinela_TPD.Models.EstadioModel", "EstadioModel")
                        .WithMany()
                        .HasForeignKey("ClaveEstadio");

                    b.Navigation("EquipoModel1");

                    b.Navigation("EquipoModel2");

                    b.Navigation("EstadioModel");
                });

            modelBuilder.Entity("Quinela_TPD.Models.MarcardorModel", b =>
                {
                    b.HasOne("Quinela_TPD.Models.JuegoModel", "JuegoModel")
                        .WithMany()
                        .HasForeignKey("ClaveJuego");

                    b.Navigation("JuegoModel");
                });

            modelBuilder.Entity("Quinela_TPD.Models.QuinielaModel", b =>
                {
                    b.HasOne("Quinela_TPD.Models.CodigoPromocionalModel", "CodigoPromocionalModel")
                        .WithMany()
                        .HasForeignKey("ClaveCodigoPromocional");

                    b.HasOne("Quinela_TPD.Models.JuegoModel", "JuegoModel")
                        .WithMany()
                        .HasForeignKey("ClaveJuego");

                    b.Navigation("CodigoPromocionalModel");

                    b.Navigation("JuegoModel");
                });
#pragma warning restore 612, 618
        }
    }
}
