using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ictx.WebApp.Api.Data.Migrations.Backgroundservice
{
    public partial class InitialAppDbContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "varchar(32)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Started = table.Column<bool>(type: "bit", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    Error = table.Column<bool>(type: "bit", nullable: false),
                    Inserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operation");
        }
    }
}
