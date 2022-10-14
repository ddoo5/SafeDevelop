using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD_lib.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Work");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Work",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    patronymic = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "Work",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    card_number = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    cvv = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false),
                    date_expiration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HolderClientId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.guid);
                    table.ForeignKey(
                        name: "FK_Cards_Clients_HolderClientId",
                        column: x => x.HolderClientId,
                        principalSchema: "Work",
                        principalTable: "Clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_HolderClientId",
                schema: "Work",
                table: "Cards",
                column: "HolderClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards",
                schema: "Work");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Work");
        }
    }
}
