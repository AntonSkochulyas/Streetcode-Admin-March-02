using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class FixJwtFinalLast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiryTime",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
