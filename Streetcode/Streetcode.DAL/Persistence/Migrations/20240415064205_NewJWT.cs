using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Persistence.Migrations
{
    public partial class NewJWT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRegister");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAdditionalInfoId",
                table: "AspNetUsers",
                column: "UserAdditionalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAdditionalInfo_UserAdditionalInfoId",
                table: "AspNetUsers",
                column: "UserAdditionalInfoId",
                principalSchema: "UserAdditionalInfo",
                principalTable: "UserAdditionalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAdditionalInfo_UserAdditionalInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserAdditionalInfoId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAdditionalInfoId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRegister_UserAdditionalInfo_UserAdditionalInfoId",
                        column: x => x.UserAdditionalInfoId,
                        principalSchema: "UserAdditionalInfo",
                        principalTable: "UserAdditionalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRegister_UserAdditionalInfoId",
                table: "UserRegister",
                column: "UserAdditionalInfoId");
        }
    }
}
