using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Postter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    HashPassword = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Moder" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "DateAdded", "Email", "HashPassword", "RoleId", "Salt" },
                values: new object[,]
                {
                    { new Guid("255f0583-5562-459b-aec9-0cd710c9548c"), new DateTime(2022, 10, 3, 14, 40, 0, 624, DateTimeKind.Utc).AddTicks(2112), "admin@gmail.com", "F3C16288193DE4109101B275139EB26A", 1, "&jJG9pdzFbKg0DEv1oeg" },
                    { new Guid("45f51610-69e2-4e1f-a2c7-914fbee0bb66"), new DateTime(2022, 10, 3, 14, 40, 0, 624, DateTimeKind.Utc).AddTicks(2132), "moder@gmail.com", "EDA47FA3EECA76390DD35EB4CB003E9C", 3, "pp#OEW%BOK8n_T2aVXTC" },
                    { new Guid("a2fbf9de-7e80-426a-bbf0-21f470b16a36"), new DateTime(2022, 10, 3, 14, 40, 0, 624, DateTimeKind.Utc).AddTicks(2130), "user@gmail.com", "820917053DE2554AFE1269F8B9C619F1", 2, "O6A6YX9^v^b0&izbX3Uu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_RoleId",
                table: "Person",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
