using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class HistoryMapFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_qr_coordinates_streetcodes_StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropIndex(
                name: "IX_qr_coordinates_StreetcodeCoordinateId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropIndex(
                name: "IX_qr_coordinates_StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropColumn(
                name: "Count",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropColumn(
                name: "StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.AddColumn<int>(
                name: "StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_qr_coordinates_StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeContentId");

            migrationBuilder.CreateIndex(
                name: "IX_qr_coordinates_StreetcodeCoordinateId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeCoordinateId");

            migrationBuilder.AddForeignKey(
                name: "FK_qr_coordinates_streetcodes_StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeContentId",
                principalSchema: "streetcode",
                principalTable: "streetcodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_qr_coordinates_streetcodes_StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropIndex(
                name: "IX_qr_coordinates_StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropIndex(
                name: "IX_qr_coordinates_StreetcodeCoordinateId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.DropColumn(
                name: "StreetcodeContentId",
                schema: "coordinates",
                table: "qr_coordinates");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                schema: "coordinates",
                table: "qr_coordinates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_qr_coordinates_StreetcodeCoordinateId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeCoordinateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_qr_coordinates_StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_qr_coordinates_streetcodes_StreetcodeId",
                schema: "coordinates",
                table: "qr_coordinates",
                column: "StreetcodeId",
                principalSchema: "streetcode",
                principalTable: "streetcodes",
                principalColumn: "Id");
        }
    }
}
