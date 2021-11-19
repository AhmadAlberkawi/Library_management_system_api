using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bvs_API.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passwort",
                table: "Admin");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Admin",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Admin",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "Passwort",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
