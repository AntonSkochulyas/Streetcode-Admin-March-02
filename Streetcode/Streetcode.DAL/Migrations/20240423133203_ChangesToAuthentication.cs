using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class ChangesToAuthentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAdditionalInfo_UserAdditionalInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAdditionalInfo",
                schema: "Users",
                table: "UserAdditionalInfo");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.RenameTable(
                name: "UserAdditionalInfo",
                schema: "Users",
                newName: "user_Additional_info",
                newSchema: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_Additional_info",
                schema: "users",
                table: "user_Additional_info",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshTokens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_ApplicationUserId",
                schema: "users",
                table: "refresh_tokens",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_user_Additional_info_UserAdditionalInfoId",
                table: "AspNetUsers",
                column: "UserAdditionalInfoId",
                principalSchema: "users",
                principalTable: "user_Additional_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_user_Additional_info_UserAdditionalInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "refresh_tokens",
                schema: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_Additional_info",
                schema: "users",
                table: "user_Additional_info");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.RenameTable(
                name: "user_Additional_info",
                schema: "users",
                newName: "UserAdditionalInfo",
                newSchema: "Users");

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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAdditionalInfo",
                schema: "Users",
                table: "UserAdditionalInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAdditionalInfo_UserAdditionalInfoId",
                table: "AspNetUsers",
                column: "UserAdditionalInfoId",
                principalSchema: "Users",
                principalTable: "UserAdditionalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
