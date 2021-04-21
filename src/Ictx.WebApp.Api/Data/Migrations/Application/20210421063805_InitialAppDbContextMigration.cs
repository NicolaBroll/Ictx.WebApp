using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ictx.WebApp.Api.Data.Migrations.Application
{
    public partial class InitialAppDbContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UfficioBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denominazione = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CodiceUfficioBase = table.Column<int>(type: "int", nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UfficioBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ufficio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UfficioBaseId = table.Column<int>(type: "int", nullable: false),
                    CodiceUfficio = table.Column<int>(type: "int", nullable: false),
                    Denominazione = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ufficio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ufficio_UfficioBase_UfficioBaseId",
                        column: x => x.UfficioBaseId,
                        principalTable: "UfficioBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Impresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UfficioId = table.Column<int>(type: "int", nullable: false),
                    CodiceImpresa = table.Column<int>(type: "int", nullable: false),
                    Denominazione = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Impresa_Ufficio_UfficioId",
                        column: x => x.UfficioId,
                        principalTable: "Ufficio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ditta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImpresaId = table.Column<int>(type: "int", nullable: false),
                    CodiceDitta = table.Column<int>(type: "int", nullable: false),
                    Denominazione = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ditta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ditta_Impresa_ImpresaId",
                        column: x => x.ImpresaId,
                        principalTable: "Impresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dipendente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DittaId = table.Column<int>(type: "int", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "char(16)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Sesso = table.Column<string>(type: "char(1)", nullable: false),
                    DataNascita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dipendente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dipendente_Ditta_DittaId",
                        column: x => x.DittaId,
                        principalTable: "Ditta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dipendente_DittaId",
                table: "Dipendente",
                column: "DittaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ditta_ImpresaId",
                table: "Ditta",
                column: "ImpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Impresa_UfficioId",
                table: "Impresa",
                column: "UfficioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ufficio_UfficioBaseId",
                table: "Ufficio",
                column: "UfficioBaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dipendente");

            migrationBuilder.DropTable(
                name: "Ditta");

            migrationBuilder.DropTable(
                name: "Impresa");

            migrationBuilder.DropTable(
                name: "Ufficio");

            migrationBuilder.DropTable(
                name: "UfficioBase");
        }
    }
}
