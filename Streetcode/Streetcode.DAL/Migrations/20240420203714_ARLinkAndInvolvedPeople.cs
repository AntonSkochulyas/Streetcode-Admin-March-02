using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class ARLinkAndInvolvedPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstagramARLink",
                schema: "streetcode",
                table: "streetcodes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvolvedPeople",
                schema: "streetcode",
                table: "streetcodes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramARLink",
                schema: "streetcode",
                table: "streetcodes");

            migrationBuilder.DropColumn(
                name: "InvolvedPeople",
                schema: "streetcode",
                table: "streetcodes");
        }
    }
}
