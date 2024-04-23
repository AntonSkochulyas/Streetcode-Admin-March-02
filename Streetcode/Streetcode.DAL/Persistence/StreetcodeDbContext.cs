using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Entities.Feedback;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Entities.Media;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Entities.Transactions;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Persistence.Configurations;
using System.Reflection.Emit;

namespace Streetcode.DAL.Persistence;

public class StreetcodeDbContext : IdentityDbContext<ApplicationUser>
{
    public StreetcodeDbContext()
    {
    }

    public StreetcodeDbContext(DbContextOptions<StreetcodeDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAdditionalInfo> UsersAdditionalInfo { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Art>? Arts { get; set; }
    public DbSet<Audio>? Audios { get; set; }
    public DbSet<DictionaryItem> DictionaryItems { get; set; }
    public DbSet<InfoBlock> InfoBlocks { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<AuthorShip> AuthorShips { get; set; }
    public DbSet<AuthorShipHyperLink> AuthorShipHyperLinks { get; set; }
    public DbSet<ToponymCoordinate>? ToponymCoordinates { get; set; }
    public DbSet<StreetcodeCoordinate>? StreetcodeCoordinates { get; set; }
    public DbSet<Fact>? Facts { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<ImageDetails>? ImageDetailses { get; set; }
    public DbSet<Partner>? Partners { get; set; }
    public DbSet<PartnerSourceLink>? PartnerSourceLinks { get; set; }
    public DbSet<RelatedFigure>? RelatedFigures { get; set; }
    public DbSet<Response>? Responses { get; set; }
    public DbSet<StreetcodeContent>? Streetcodes { get; set; }
    public DbSet<Subtitle>? Subtitles { get; set; }
    public DbSet<StatisticRecord>? StatisticRecords { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<Term>? Terms { get; set; }
    public DbSet<RelatedTerm>? RelatedTerms { get; set; }
    public DbSet<Text>? Texts { get; set; }
    public DbSet<TimelineItem>? TimelineItems { get; set; }
    public DbSet<Toponym>? Toponyms { get; set; }
    public DbSet<TransactionLink>? TransactionLinks { get; set; }
    public DbSet<Video>? Videos { get; set; }
    public DbSet<StreetcodeCategoryContent>? StreetcodeCategoryContent { get; set; }
    public DbSet<StreetcodeArt>? StreetcodeArts { get; set; }
    public DbSet<StreetcodeTagIndex>? StreetcodeTagIndices { get; set; }
    public DbSet<TeamMember>? TeamMembers { get; set; }
    public DbSet<TeamMemberLink>? TeamMemberLinks { get; set; }
    public DbSet<Positions>? Positions { get; set; }
    public DbSet<News>? News { get; set; }
    public DbSet<SourceLinkCategory>? SourceLinks { get; set; }
    public DbSet<StreetcodeImage>? StreetcodeImages { get; set; }
    public DbSet<StreetcodePartner>? StreetcodePartners { get; set; }
    public DbSet<TeamMemberPositions>? TeamMemberPosition { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseCollation("SQL_Ukrainian_CP1251_CI_AS");

        builder.ApplyConfiguration(new ArtEntityConfiguration());

        builder.ApplyConfiguration(new AudioEntityConfiguration());

        builder.ApplyConfiguration(new CoordinateEntityConfiguration());

        builder.ApplyConfiguration(new FactEntityConfiguration());

        builder.ApplyConfiguration(new ImageDetailsEntityConfiguration());

        builder.ApplyConfiguration(new ImageEntityConfiguration());

        builder.ApplyConfiguration(new NewsEntityConfiguration());

        builder.ApplyConfiguration(new PartnerEntityConfiguration());

        builder.ApplyConfiguration(new PartnerSourceLinkEntityConfiguration());

        builder.ApplyConfiguration(new PersonStreetcodeEntityConfiguration());

        builder.ApplyConfiguration(new PositionsEntityConfiguration());

        builder.ApplyConfiguration(new RelatedFigureEntityConfiguration());

        builder.ApplyConfiguration(new RelatedTermEntityConfiguration());

        builder.ApplyConfiguration(new ResponseEntityConfiguration());

        builder.ApplyConfiguration(new SourceLinkCategoryEntityConfiguration());

        builder.ApplyConfiguration(new StatisticRecordEntityConfiguration());

        builder.ApplyConfiguration(new StreetcodeArtEntityConfiguration());

        builder.ApplyConfiguration(new StreetcodeCategoryContentEntityConfiguration());

        builder.ApplyConfiguration(new StreetcodeContentEntityConfiguration());

        builder.ApplyConfiguration(new StreetcodeTagIndexEntityConfiguration());

        builder.ApplyConfiguration(new SubtitleEntityConfiguration());

        builder.ApplyConfiguration(new TagEntityConfiguration());

        builder.ApplyConfiguration(new TeamMemberEntityConfiguration());

        builder.ApplyConfiguration(new TeamMemberLinkEntityConfiguration());

        builder.ApplyConfiguration(new TeamMemberPositionsEntityConfigurations());

        builder.ApplyConfiguration(new TermEntityConfiguration());

        builder.ApplyConfiguration(new TextEntityConfiguration());

        builder.ApplyConfiguration(new TimelineItemEntityConfiguration());

        builder.ApplyConfiguration(new ToponymEntityConfiguration());

        builder.ApplyConfiguration(new TransactionLinkEntityConfiguration());

        builder.ApplyConfiguration(new UserEntityConfiguration());

        builder.ApplyConfiguration(new VideoEntityConfiguration());

        builder.ApplyConfiguration(new DictionaryItemConfiguration());

        builder.ApplyConfiguration(new InfoBlockConfiguration());

        builder.ApplyConfiguration(new ArticleConfiguration());

        builder.ApplyConfiguration(new AuthorShipConfiguration());

        builder.ApplyConfiguration(new AuthorShipHyperLinkConfiguration());
    }
}
