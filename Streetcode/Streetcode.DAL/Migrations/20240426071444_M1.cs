﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "media");

            migrationBuilder.EnsureSchema(
                name: "add_content");

            migrationBuilder.EnsureSchema(
                name: "streetcode");

            migrationBuilder.EnsureSchema(
                name: "news");

            migrationBuilder.EnsureSchema(
                name: "partners");

            migrationBuilder.EnsureSchema(
                name: "team");

            migrationBuilder.EnsureSchema(
                name: "coordinates");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.EnsureSchema(
                name: "feedback");

            migrationBuilder.EnsureSchema(
                name: "sources");

            migrationBuilder.EnsureSchema(
                name: "toponyms");

            migrationBuilder.EnsureSchema(
                name: "transactions");

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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "audios",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlobName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audios", x => x.Id);
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
                name: "images",
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
                    table.PrimaryKey("PK_images", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "positions",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "responses",
                schema: "feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "add_content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "toponyms",
                schema: "toponyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oblast = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminRegionOld = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AdminRegionNew = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Gromada = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Community = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StreetType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toponyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_Additional_info",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ThirdName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Additional_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorShipHyperLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorShipId = table.Column<int>(type: "int", nullable: true)
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
                name: "arts",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arts_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "image_details",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_image_details_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "news",
                schema: "news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.Id);
                    table.ForeignKey(
                        name: "FK_news_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "partners",
                schema: "partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LogoId = table.Column<int>(type: "int", nullable: false),
                    IsKeyPartner = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibleEverywhere = table.Column<bool>(type: "bit", nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UrlTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partners_images_LogoId",
                        column: x => x.LogoId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "source_link_categories",
                schema: "sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_source_link_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_source_link_categories_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_members",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_members_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "streetcodes",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Teaser = table.Column<string>(type: "nvarchar(650)", maxLength: 650, nullable: true),
                    DateString = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransliterationUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EventStartOrPersonBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEndOrPersonDeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstagramARLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InvolvedPeople = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AudioId = table.Column<int>(type: "int", nullable: true),
                    ImageBlackAndWhiteId = table.Column<int>(type: "int", nullable: true),
                    ImageAnimatedId = table.Column<int>(type: "int", nullable: true),
                    ImageForLinkId = table.Column<int>(type: "int", nullable: true),
                    StreetcodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_streetcodes_audios_AudioId",
                        column: x => x.AudioId,
                        principalSchema: "media",
                        principalTable: "audios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcodes_images_main_ImageAnimatedId",
                        column: x => x.ImageAnimatedId,
                        principalSchema: "media",
                        principalTable: "images_main",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_streetcodes_images_main_ImageBlackAndWhiteId",
                        column: x => x.ImageBlackAndWhiteId,
                        principalSchema: "media",
                        principalTable: "images_main",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcodes_images_main_ImageForLinkId",
                        column: x => x.ImageForLinkId,
                        principalSchema: "media",
                        principalTable: "images_main",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "related_terms",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_related_terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_related_terms_terms_TermId",
                        column: x => x.TermId,
                        principalSchema: "streetcode",
                        principalTable: "terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAdditionalInfoId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_user_Additional_info_UserAdditionalInfoId",
                        column: x => x.UserAdditionalInfoId,
                        principalSchema: "users",
                        principalTable: "user_Additional_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partner_source_links",
                schema: "partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoType = table.Column<byte>(type: "tinyint", nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_source_links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partner_source_links_partners_PartnerId",
                        column: x => x.PartnerId,
                        principalSchema: "partners",
                        principalTable: "partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_member_links",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoType = table.Column<byte>(type: "tinyint", nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TeamMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_member_links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_member_links_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "team",
                        principalTable: "team_members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_member_positions",
                schema: "team",
                columns: table => new
                {
                    TeamMemberId = table.Column<int>(type: "int", nullable: false),
                    PositionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_member_positions", x => new { x.TeamMemberId, x.PositionsId });
                    table.ForeignKey(
                        name: "FK_team_member_positions_positions_PositionsId",
                        column: x => x.PositionsId,
                        principalSchema: "team",
                        principalTable: "positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_member_positions_team_members_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "team",
                        principalTable: "team_members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "coordinates",
                schema: "add_content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Longtitude = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CoordinateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetcodeId = table.Column<int>(type: "int", nullable: true),
                    ToponymId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_coordinates_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coordinates_toponyms_ToponymId",
                        column: x => x.ToponymId,
                        principalSchema: "toponyms",
                        principalTable: "toponyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "facts",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FactContent = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    ImageDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_facts_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facts_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "related_figures",
                schema: "streetcode",
                columns: table => new
                {
                    ObserverId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_related_figures", x => new { x.ObserverId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_related_figures_streetcodes_ObserverId",
                        column: x => x.ObserverId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_related_figures_streetcodes_TargetId",
                        column: x => x.TargetId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_art",
                schema: "streetcode",
                columns: table => new
                {
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    ArtId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_art", x => new { x.ArtId, x.StreetcodeId });
                    table.ForeignKey(
                        name: "FK_streetcode_art_arts_ArtId",
                        column: x => x.ArtId,
                        principalSchema: "media",
                        principalTable: "arts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_art_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_image",
                schema: "streetcode",
                columns: table => new
                {
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_image", x => new { x.ImageId, x.StreetcodeId });
                    table.ForeignKey(
                        name: "FK_streetcode_image_images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "media",
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_image_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_partners",
                schema: "streetcode",
                columns: table => new
                {
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_partners", x => new { x.PartnerId, x.StreetcodeId });
                    table.ForeignKey(
                        name: "FK_streetcode_partners_partners_PartnerId",
                        column: x => x.PartnerId,
                        principalSchema: "partners",
                        principalTable: "partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_partners_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_source_link_categories",
                schema: "sources",
                columns: table => new
                {
                    SourceLinkCategoryId = table.Column<int>(type: "int", nullable: false),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_source_link_categories", x => new { x.SourceLinkCategoryId, x.StreetcodeId });
                    table.ForeignKey(
                        name: "FK_streetcode_source_link_categories_source_link_categories_SourceLinkCategoryId",
                        column: x => x.SourceLinkCategoryId,
                        principalSchema: "sources",
                        principalTable: "source_link_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_source_link_categories_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_tag_index",
                schema: "add_content",
                columns: table => new
                {
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_tag_index", x => new { x.StreetcodeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_streetcode_tag_index_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_tag_index_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "add_content",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "streetcode_toponym",
                schema: "streetcode",
                columns: table => new
                {
                    StreetcodeId = table.Column<int>(type: "int", nullable: false),
                    ToponymId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streetcode_toponym", x => new { x.StreetcodeId, x.ToponymId });
                    table.ForeignKey(
                        name: "FK_streetcode_toponym_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_streetcode_toponym_toponyms_ToponymId",
                        column: x => x.ToponymId,
                        principalSchema: "toponyms",
                        principalTable: "toponyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subtitles",
                schema: "add_content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubtitleText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subtitles_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "texts",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", maxLength: 15000, nullable: false),
                    AdditionalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_texts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_texts_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeline_items",
                schema: "streetcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateViewPattern = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Context = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeline_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_timeline_items_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_links",
                schema: "transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_links_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetcodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_videos_streetcodes_StreetcodeId",
                        column: x => x.StreetcodeId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "qr_coordinates",
                schema: "coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QrId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StreetcodeCoordinateId = table.Column<int>(type: "int", nullable: false),
                    StreetcodeContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qr_coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qr_coordinates_coordinates_StreetcodeCoordinateId",
                        column: x => x.StreetcodeCoordinateId,
                        principalSchema: "add_content",
                        principalTable: "coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qr_coordinates_streetcodes_StreetcodeContentId",
                        column: x => x.StreetcodeContentId,
                        principalSchema: "streetcode",
                        principalTable: "streetcodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_arts_ImageId",
                schema: "media",
                table: "arts",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAdditionalInfoId",
                table: "AspNetUsers",
                column: "UserAdditionalInfoId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorShipHyperLinks_AuthorShipId",
                table: "AuthorShipHyperLinks",
                column: "AuthorShipId");

            migrationBuilder.CreateIndex(
                name: "IX_coordinates_StreetcodeId",
                schema: "add_content",
                table: "coordinates",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_coordinates_ToponymId",
                schema: "add_content",
                table: "coordinates",
                column: "ToponymId",
                unique: true,
                filter: "[ToponymId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_facts_ImageId",
                schema: "streetcode",
                table: "facts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_facts_StreetcodeId_OrderNumber",
                schema: "streetcode",
                table: "facts",
                columns: new[] { "StreetcodeId", "OrderNumber" },
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_image_details_ImageId",
                schema: "media",
                table: "image_details",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_news_ImageId",
                schema: "news",
                table: "news",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_news_URL",
                schema: "news",
                table: "news",
                column: "URL",
                unique: true,
                filter: "[URL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_partner_source_links_PartnerId",
                schema: "partners",
                table: "partner_source_links",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_partners_LogoId",
                schema: "partners",
                table: "partners",
                column: "LogoId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_ApplicationUserId",
                schema: "users",
                table: "refresh_tokens",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_related_figures_TargetId",
                schema: "streetcode",
                table: "related_figures",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_related_terms_TermId",
                schema: "streetcode",
                table: "related_terms",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_source_link_categories_ImageId",
                schema: "sources",
                table: "source_link_categories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_art_ArtId_StreetcodeId",
                schema: "streetcode",
                table: "streetcode_art",
                columns: new[] { "ArtId", "StreetcodeId" });

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_art_StreetcodeId",
                schema: "streetcode",
                table: "streetcode_art",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_image_StreetcodeId",
                schema: "streetcode",
                table: "streetcode_image",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_partners_StreetcodeId",
                schema: "streetcode",
                table: "streetcode_partners",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_source_link_categories_StreetcodeId",
                schema: "sources",
                table: "streetcode_source_link_categories",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_tag_index_TagId",
                schema: "add_content",
                table: "streetcode_tag_index",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcode_toponym_ToponymId",
                schema: "streetcode",
                table: "streetcode_toponym",
                column: "ToponymId");

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_AudioId",
                schema: "streetcode",
                table: "streetcodes",
                column: "AudioId",
                unique: true,
                filter: "[AudioId] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_Index",
                schema: "streetcode",
                table: "streetcodes",
                column: "Index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_streetcodes_TransliterationUrl",
                schema: "streetcode",
                table: "streetcodes",
                column: "TransliterationUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subtitles_StreetcodeId",
                schema: "add_content",
                table: "subtitles",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_team_member_links_TeamMemberId",
                schema: "team",
                table: "team_member_links",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_team_member_positions_PositionsId",
                schema: "team",
                table: "team_member_positions",
                column: "PositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_team_members_ImageId",
                schema: "team",
                table: "team_members",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_texts_StreetcodeId",
                schema: "streetcode",
                table: "texts",
                column: "StreetcodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_timeline_items_StreetcodeId",
                schema: "streetcode",
                table: "timeline_items",
                column: "StreetcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_links_StreetcodeId",
                schema: "transactions",
                table: "transaction_links",
                column: "StreetcodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_videos_StreetcodeId",
                schema: "media",
                table: "videos",
                column: "StreetcodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorShipHyperLinks");

            migrationBuilder.DropTable(
                name: "DictionaryItems");

            migrationBuilder.DropTable(
                name: "facts",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "image_details",
                schema: "media");

            migrationBuilder.DropTable(
                name: "InfoBlocks");

            migrationBuilder.DropTable(
                name: "news",
                schema: "news");

            migrationBuilder.DropTable(
                name: "partner_source_links",
                schema: "partners");

            migrationBuilder.DropTable(
                name: "qr_coordinates",
                schema: "coordinates");

            migrationBuilder.DropTable(
                name: "refresh_tokens",
                schema: "users");

            migrationBuilder.DropTable(
                name: "related_figures",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "related_terms",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "responses",
                schema: "feedback");

            migrationBuilder.DropTable(
                name: "streetcode_art",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "streetcode_image",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "streetcode_partners",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "streetcode_source_link_categories",
                schema: "sources");

            migrationBuilder.DropTable(
                name: "streetcode_tag_index",
                schema: "add_content");

            migrationBuilder.DropTable(
                name: "streetcode_toponym",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "subtitles",
                schema: "add_content");

            migrationBuilder.DropTable(
                name: "team_member_links",
                schema: "team");

            migrationBuilder.DropTable(
                name: "team_member_positions",
                schema: "team");

            migrationBuilder.DropTable(
                name: "texts",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "timeline_items",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "transaction_links",
                schema: "transactions");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "media");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AuthorShips");

            migrationBuilder.DropTable(
                name: "coordinates",
                schema: "add_content");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "terms",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "arts",
                schema: "media");

            migrationBuilder.DropTable(
                name: "partners",
                schema: "partners");

            migrationBuilder.DropTable(
                name: "source_link_categories",
                schema: "sources");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "add_content");

            migrationBuilder.DropTable(
                name: "positions",
                schema: "team");

            migrationBuilder.DropTable(
                name: "team_members",
                schema: "team");

            migrationBuilder.DropTable(
                name: "streetcodes",
                schema: "streetcode");

            migrationBuilder.DropTable(
                name: "toponyms",
                schema: "toponyms");

            migrationBuilder.DropTable(
                name: "user_Additional_info",
                schema: "users");

            migrationBuilder.DropTable(
                name: "images",
                schema: "media");

            migrationBuilder.DropTable(
                name: "audios",
                schema: "media");

            migrationBuilder.DropTable(
                name: "images_main",
                schema: "media");
        }
    }
}
