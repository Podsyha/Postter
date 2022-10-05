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
                    Name = table.Column<string>(type: "text", nullable: false)
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
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    About = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: true),
                    HashPassword = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
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
                columns: new[] { "Id", "About", "DateAdded", "Email", "HashPassword", "ImageUri", "IsActive", "Name", "RoleId", "Salt" },
                values: new object[,]
                {
                    { new Guid("3087656c-0a96-4b90-a4f3-cc071478435f"), null, new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2115), "user@gmail.com", "AA1F76ABBDD756030D2245FE00DC701A", "testlink.com/testdirectory/testimage.png", true, "User Prostetskiy", 2, "KJ@ZG&34_de!3QP5BKJ#" },
                    { new Guid("b24ee270-f4a4-431a-ae16-6c150dc117c7"), null, new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2118), "moder@gmail.com", "7AAE39726A38A1FD2C0B7DD94EBA9379", "testlink.com/testdirectory/testimage.png", true, "Moderator Zloy i Derzkiy", 3, "368fV5ZBb4se+25*5(RN" },
                    { new Guid("ec4e0eea-6df1-4892-8fde-be8334066942"), null, new DateTime(2022, 10, 5, 18, 38, 19, 327, DateTimeKind.Utc).AddTicks(2075), "admin@gmail.com", "554F8AC9FB309CE8385D473E683952F6", "testlink.com/testdirectory/testimage.png", true, "Admin Adminskiy", 1, "$t3*l0aNDebRhuY22#yy" }
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
