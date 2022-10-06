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

            migrationBuilder.CreateTable(
                name: "PostEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntity_Person_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_Person_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentEntity_PostEntity_PostId",
                        column: x => x.PostId,
                        principalTable: "PostEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeEntity_Person_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeEntity_PostEntity_PostId",
                        column: x => x.PostId,
                        principalTable: "PostEntity",
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
                    { new Guid("5937eb5f-4278-467e-977b-e7205ed5fd9b"), null, new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(625), "admin@gmail.com", "C7284652D08DCF8A1EC9AE5B6FD46E65", "testlink.com/testdirectory/testimage.png", true, "Admin Adminskiy", 1, "t(quhPhBQuXC9amhy(hk" },
                    { new Guid("8b1d22ce-e48e-4108-bf00-24c34ee6228b"), null, new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(649), "moder@gmail.com", "1F533E735DB40963C78494A1E988DC07", "testlink.com/testdirectory/testimage.png", true, "Moderator Zloy i Derzkiy", 3, "&iJgwfjwAE!Hp35wEy+e" },
                    { new Guid("ff99309a-8e95-469d-b454-4d5b45819314"), null, new DateTime(2022, 10, 6, 11, 33, 26, 737, DateTimeKind.Utc).AddTicks(647), "user@gmail.com", "87753F78463D2521ECDA7B9B8DD32366", "testlink.com/testdirectory/testimage.png", true, "User Prostetskiy", 2, "oB#G%h3n^21et9!q(7ok" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_AuthorId",
                table: "CommentEntity",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_PostId",
                table: "CommentEntity",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_AuthorId",
                table: "LikeEntity",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_PostId",
                table: "LikeEntity",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_RoleId",
                table: "Person",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntity_AuthorId",
                table: "PostEntity",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "LikeEntity");

            migrationBuilder.DropTable(
                name: "PostEntity");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
