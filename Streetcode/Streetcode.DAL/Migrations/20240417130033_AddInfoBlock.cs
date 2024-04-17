using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class AddInfoBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 15000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AuthorShipHyperLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorShips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorShipHyperLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorShipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorShipHyperLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorShipHyperLinks_AuthorShips_AuthorShipId",
                        column: x => x.AuthorShipId,
                        principalTable: "AuthorShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    AuthorShipId = table.Column<int>(type: "int", nullable: true),
                    VideoURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoBlocks_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoBlocks_AuthorShips_AuthorShipId",
                        column: x => x.AuthorShipId,
                        principalTable: "AuthorShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfoBlocks_terms_TermId",
                        column: x => x.TermId,
                        principalSchema: "streetcode",
                        principalTable: "terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorShipHyperLinks_AuthorShipId",
                table: "AuthorShipHyperLinks",
                column: "AuthorShipId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoBlocks_ArticleId",
                table: "InfoBlocks",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoBlocks_AuthorShipId",
                table: "InfoBlocks",
                column: "AuthorShipId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoBlocks_TermId",
                table: "InfoBlocks",
                column: "TermId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorShipHyperLinks");

            migrationBuilder.DropTable(
                name: "DictionaryItems");

            migrationBuilder.DropTable(
                name: "InfoBlocks");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AuthorShips");
        }
    }
}
