using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Persistence.Migrations
{
    public partial class ChangesToStreetcodeContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransliterationUrl",
                schema: "streetcode",
                table: "streetcodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                schema: "streetcode",
                table: "streetcodes",
                type: "nvarchar(33)",
                maxLength: 33,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "images_main",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlobName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images_main", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageAnimatedId",
                unique: true,
                filter: "[ImageAnimatedId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageBlackAndWhiteId",
                unique: true,
                filter: "[ImageBlackAndWhiteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageForLinkId",
                unique: true,
                filter: "[ImageForLinkId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_streetcodes_images_main_ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageAnimatedId",
                principalSchema: "media",
                principalTable: "images_main",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_streetcodes_images_main_ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageBlackAndWhiteId",
                principalSchema: "media",
                principalTable: "images_main",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_streetcodes_images_main_ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes",
                column: "ImageForLinkId",
                principalSchema: "media",
                principalTable: "images_main",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_streetcodes_images_main_ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropForeignKey(
                name: "FK_streetcodes_images_main_ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropForeignKey(
                name: "FK_streetcodes_images_main_ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropTable(
                name: "images_main",
                schema: "media");

            migrationBuilder.DropIndex(
                name: "IX_streetcodes_ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropIndex(
                name: "IX_streetcodes_ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropIndex(
                name: "IX_streetcodes_ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropColumn(
                name: "ImageAnimatedId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropColumn(
                name: "ImageBlackAndWhiteId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropColumn(
                name: "ImageForLinkId",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.AlterColumn<string>(
                name: "TransliterationUrl",
                schema: "streetcode",
                table: "streetcodes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
