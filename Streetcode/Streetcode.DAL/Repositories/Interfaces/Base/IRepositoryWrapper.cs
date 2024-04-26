using System.Transactions;
using Repositories.Interfaces;
using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
using Streetcode.DAL.Repositories.Interfaces.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Authentication;
using Streetcode.DAL.Repositories.Interfaces.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Newss;
using Streetcode.DAL.Repositories.Interfaces.Partners;
using Streetcode.DAL.Repositories.Interfaces.Source;
using Streetcode.DAL.Repositories.Interfaces.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Team;
using Streetcode.DAL.Repositories.Interfaces.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Transactions;
using Streetcode.DAL.Repositories.Interfaces.Users;
namespace Streetcode.DAL.Repositories.Interfaces.Base;

public interface IRepositoryWrapper
{
    IApplicationUserRepository ApplicationUserRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IFactRepository FactRepository { get; }
    IArtRepository ArtRepository { get; }
    IStreetcodeArtRepository StreetcodeArtRepository { get; }
    IVideoRepository VideoRepository { get; }
    IImageRepository ImageRepository { get; }
    IImageDetailsRepository ImageDetailsRepository { get; }
    IImageMainRepository ImageMainRepository { get; }
    IAudioRepository AudioRepository { get; }
    IStreetcodeCoordinateRepository StreetcodeCoordinateRepository { get; }
    IPartnersRepository PartnersRepository { get; }
    ISourceCategoryRepository SourceCategoryRepository { get; }
    IStreetcodeCategoryContentRepository StreetcodeCategoryContentRepository { get; }
    IRelatedFigureRepository RelatedFigureRepository { get; }
    IStreetcodeRepository StreetcodeRepository { get; }
    ISubtitleRepository SubtitleRepository { get; }
    IStatisticRecordRepository StatisticRecordRepository { get; }
    ITagRepository TagRepository { get; }
    ITeamRepository TeamRepository { get; }
    ITeamPositionRepository TeamPositionRepository { get; }
    ITeamLinkRepository TeamLinkRepository { get; }
    ITermRepository TermRepository { get; }
    IRelatedTermRepository RelatedTermRepository { get; }
    ITextRepository TextRepository { get; }
    ITimelineRepository TimelineRepository { get; }
    IToponymRepository ToponymRepository { get; }
    ITransactLinksRepository TransactLinksRepository { get; }
    IPartnerSourceLinkRepository PartnerSourceLinkRepository { get; }
    IStreetcodeTagIndexRepository StreetcodeTagIndexRepository { get; }
    IPartnerStreetcodeRepository PartnerStreetcodeRepository { get;  }
    INewsRepository NewsRepository { get; }
    IPositionRepository PositionRepository { get; }
    IStreetcodeToponymRepository StreetcodeToponymRepository { get; }
    IStreetcodeImageRepository StreetcodeImageRepository { get; }
    IDictionaryItemRepository DictionaryItemRepository { get; }
    IInfoBlockRepository InfoBlockRepository { get; }
    IArticleRepository ArticleRepository { get; }
    IAuthorShipRepository AuthorShipRepository { get; }
    IAuthorHyperLinkRepository AuthorShipHyperLinkRepository { get; }

    IUserAdditionalInfoRepository UserAdditionalInfoRepository { get; }

    public int SaveChanges();

    public Task<int> SaveChangesAsync();

    public TransactionScope BeginTransaction();
}
