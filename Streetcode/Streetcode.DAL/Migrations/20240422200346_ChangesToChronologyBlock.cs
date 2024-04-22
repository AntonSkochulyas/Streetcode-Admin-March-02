using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class ChangesToChronologyBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalContextsTimelines");

            migrationBuilder.DropTable(
                name: "historical_contexts",
                schema: "timeline");

            migrationBuilder.RenameTable(
                name: "timeline_items",
                schema: "timeline",
                newName: "timeline_items",
                newSchema: "streetcode");

            migrationBuilder.AddColumn<string>(
                name: "Context",
                schema: "streetcode",
                table: "timeline_items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Context",
                schema: "streetcode",
                table: "timeline_items");

            migrationBuilder.EnsureSchema(
                name: "timeline");

            migrationBuilder.RenameTable(
                name: "timeline_items",
                schema: "streetcode",
                newName: "timeline_items",
                newSchema: "timeline");

            migrationBuilder.CreateTable(
                name: "historical_contexts",
                schema: "timeline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historical_contexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalContextsTimelines",
                columns: table => new
                {
                    TimelineId = table.Column<int>(type: "int", nullable: false),
                    HistoricalContextId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalContextsTimelines", x => new { x.TimelineId, x.HistoricalContextId });
                    table.ForeignKey(
                        name: "FK_HistoricalContextsTimelines_historical_contexts_HistoricalContextId",
                        column: x => x.HistoricalContextId,
                        principalSchema: "timeline",
                        principalTable: "historical_contexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricalContextsTimelines_timeline_items_TimelineId",
                        column: x => x.TimelineId,
                        principalSchema: "timeline",
                        principalTable: "timeline_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalContextsTimelines_HistoricalContextId",
                table: "HistoricalContextsTimelines",
                column: "HistoricalContextId");
        }
    }
}
