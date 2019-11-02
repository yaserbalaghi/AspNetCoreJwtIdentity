using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrossCutting.Identity.Jwt.Migrations
{
    public partial class alter_users_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08a029db-1b12-4535-863a-f4ad30422219"),
                column: "SecurityStamp",
                value: "83ff1d3e-77fd-4bb6-9040-2fbc4e3f4eeb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08a029db-1b12-4535-863a-f4ad30422219"),
                column: "SecurityStamp",
                value: "00496754-4011-4642-a7a0-59a310903ad7");
        }
    }
}
