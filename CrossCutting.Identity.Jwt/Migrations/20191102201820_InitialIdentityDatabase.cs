using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossCutting.Identity.Jwt.Migrations
{
    public partial class InitialIdentityDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                    table.UniqueConstraint("AK_Users_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "system super administrator", "SUPERADMIN" },
                    { 2, "system administrator", "ADMIN" },
                    { 3, "blog writer", "WRITER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "LastLoginDate", "Password", "SecurityStamp", "UserName" },
                values: new object[] { new Guid("08a029db-1b12-4535-863a-f4ad30422219"), "Eng.balaghi@yahoo.com", "Yaser Balaghi", null, "123456", "1b8f820c-87dd-4fd7-b890-ed9191d0231f", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("08a029db-1b12-4535-863a-f4ad30422219"), 2 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("08a029db-1b12-4535-863a-f4ad30422219"), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
