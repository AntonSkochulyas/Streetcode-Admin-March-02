// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Interfaces.Logging;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl
{
    /// <summary>
    /// Handler, that handles a process of getting a news and links by given url.
    /// </summary>
    public class GetNewsAndLinksByUrlHandler : IRequestHandler<GetNewsAndLinksByUrlQuery, Result<NewsDtoWithURLs>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Blob service
        private readonly IBlobService _blobService;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetNewsAndLinksByUrlHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _blobService = blobService;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets a news and links by given url.
        /// </summary>
        /// <param name="request">
        /// Request with news and links url to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A NewsDtoWithURLs, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<NewsDtoWithURLs>> Handle(GetNewsAndLinksByUrlQuery request, CancellationToken cancellationToken)
        {
            string url = request.Url;
            var newsDTO = _mapper.Map<NewsDto>(await _repositoryWrapper.NewsRepository.GetFirstOrDefaultAsync(
                predicate: sc => sc.URL == url,
                include: scl => scl
                    .Include(sc => sc.Image)));

            if (newsDTO is null)
            {
                string errorMsg = string.Format(NewsErrors.GetNewsAndLinksByUrlHandlerCanNotFindANewsWithGivenURLError, request.Url);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (newsDTO.Image is not null)
            {
                newsDTO.Image.Base64 = _blobService.FindFileInStorageAsBase64(newsDTO.Image.BlobName);
            }

            var news = (await _repositoryWrapper.NewsRepository.GetAllAsync()).ToList();
            var newsIndex = news.FindIndex(x => x.Id == newsDTO.Id);
            string? prevNewsLink = null;
            string? nextNewsLink = null;

            if (newsIndex != 0)
            {
                prevNewsLink = news[newsIndex - 1].URL;
            }

            if (newsIndex != news.Count - 1)
            {
                nextNewsLink = news[newsIndex + 1].URL;
            }

            var randomNewsTitleAndLink = new RandomNewsDto();

            var arrCount = news.Count;
            if (arrCount > 3)
            {
                if (newsIndex + 1 == arrCount - 1 || newsIndex == arrCount - 1)
                {
                    randomNewsTitleAndLink.RandomNewsUrl = news[newsIndex - 2].URL;
                    randomNewsTitleAndLink.Title = news[newsIndex - 2].Title;
                }
                else
                {
                    randomNewsTitleAndLink.RandomNewsUrl = news[arrCount - 1].URL;
                    randomNewsTitleAndLink.Title = news[arrCount - 1].Title;
                }
            }
            else
            {
                randomNewsTitleAndLink.RandomNewsUrl = news[newsIndex].URL;
                randomNewsTitleAndLink.Title = news[newsIndex].Title;
            }

            var newsDTOWithUrls = new NewsDtoWithURLs();
            newsDTOWithUrls.RandomNews = randomNewsTitleAndLink;
            newsDTOWithUrls.News = newsDTO;
            newsDTOWithUrls.NextNewsUrl = nextNewsLink;
            newsDTOWithUrls.PrevNewsUrl = prevNewsLink;

            return Result.Ok(newsDTOWithUrls);
        }
    }
}