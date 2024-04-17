using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.GetByFilter
{
    public class GetStreetcodeByFilterHandler : IRequestHandler<GetStreetcodeByFilterQuery, Result<List<StreetcodeFilterResultDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetStreetcodeByFilterHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<List<StreetcodeFilterResultDto>>> Handle(GetStreetcodeByFilterQuery request, CancellationToken cancellationToken)
        {
            string searchQuery = request.Filter.SearchQuery ?? "";

            var results = new List<StreetcodeFilterResultDto>();

            var streetcodes = await _repositoryWrapper.StreetcodeRepository.GetAllAsync(
                 predicate: x =>
                                (x.Status == DAL.Enums.StreetcodeStatus.Published) &&
                                (x.Title != null ? x.Title.Contains(searchQuery) : false ||
                                (x.Alias != null && x.Alias.Contains(searchQuery)) ||
                                (x.Teaser != null ? x.Teaser.Contains(searchQuery) : false)));

            foreach (var streetcode in streetcodes)
            {
                if (streetcode.Title != null && streetcode.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(streetcode, streetcode.Title));
                    continue;
                }

                if (!string.IsNullOrEmpty(streetcode.Alias) && streetcode.Alias.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(streetcode, streetcode.Alias));
                    continue;
                }

                if (streetcode.Teaser != null && streetcode.Teaser.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(streetcode, streetcode.Teaser));
                    continue;
                }

                if (streetcode.TransliterationUrl != null && streetcode.TransliterationUrl.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(streetcode, streetcode.TransliterationUrl));
                }
            }

            foreach (var text in await _repositoryWrapper.TextRepository.GetAllAsync(
          include: i => i.Include(x => x.Streetcode ?? new StreetcodeContent()),
          predicate: x => (x.Streetcode != null ? x.Streetcode.Status : null) == DAL.Enums.StreetcodeStatus.Published))
            {
                if (text.Title != null && text.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(text.Streetcode ?? new StreetcodeContent(), text.Title, "Текст", "text"));
                    continue;
                }

                if (!string.IsNullOrEmpty(text.TextContent) && text.TextContent.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(text.Streetcode ?? new StreetcodeContent(), text.TextContent, "Текст", "text"));
                }
            }

            foreach (var fact in await _repositoryWrapper.FactRepository.GetAllAsync(
            include: i => i.Include(x => x.Streetcode ?? new StreetcodeContent()),
            predicate: x => (x.Streetcode != null && x.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published)))

            {
                if ((fact.Title != null && fact.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) || (fact.FactContent != null && fact.FactContent.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(CreateFilterResult(fact.Streetcode ?? new StreetcodeContent(), fact.Title ?? "", "Wow-факти", "wow-facts"));
                }
            }

            foreach (var timelineItem in await _repositoryWrapper.TimelineRepository.GetAllAsync(
             include: i => i.Include(x => x.Streetcode ?? new StreetcodeContent()),
             predicate: x => (x.Streetcode != null && x.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published)))
            {
                if ((timelineItem.Title != null && timelineItem.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
     || (!string.IsNullOrEmpty(timelineItem.Description) && timelineItem.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(CreateFilterResult(timelineItem.Streetcode ?? new StreetcodeContent(), timelineItem.Title, "Хронологія", "timeline"));
                }

            }

            foreach (var streetcodeArt in await _repositoryWrapper.ArtRepository.GetAllAsync(
                    include: i => i.Include(x => x.StreetcodeArts),
                    predicate: x => x.StreetcodeArts.Any(art => art.Streetcode != null && art.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published)))
            {
                if (!string.IsNullOrEmpty(streetcodeArt.Description) && streetcodeArt.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    streetcodeArt.StreetcodeArts.ForEach(art =>
                    {
                        if (art.Streetcode == null)
                        {
                            return;
                        }

                        results.Add(CreateFilterResult(art.Streetcode, streetcodeArt.Description, "Арт-галерея", "art-gallery"));
                    });
                    continue;
                }
            }

            return results;
        }

        private StreetcodeFilterResultDto CreateFilterResult(StreetcodeContent streetcode, string content, string? sourceName = null, string? blockName = null)
        {
            return new StreetcodeFilterResultDto
            {
                StreetcodeId = streetcode.Id,
                StreetcodeTransliterationUrl = streetcode.TransliterationUrl,
                StreetcodeIndex = streetcode.Index,
                BlockName = blockName,
                Content = content,
                SourceName = sourceName,
            };
        }
    }
}
