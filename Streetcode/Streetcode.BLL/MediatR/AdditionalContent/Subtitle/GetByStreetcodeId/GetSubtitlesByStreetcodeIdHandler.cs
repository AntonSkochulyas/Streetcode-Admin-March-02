// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;
using Streetcode.BLL.MediatR.ResultVariations;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetByStreetcodeId
{
    /// <summary>
    /// Handler, that get and returns subtitle by streetcode id, or error, if it was while getting process.
    /// </summary>
    public class GetSubtitlesByStreetcodeIdHandler : IRequestHandler<GetSubtitlesByStreetcodeIdQuery, Result<SubtitleDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public GetSubtitlesByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that find and returns a SubtitleDto by streetcode id, or error, if it was while getting process.
        /// </summary>
        /// <param name="request">
        /// Request with id to finding subtitle.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token for cancelling operation if it needed.
        /// </param>
        /// <returns>
        /// A SubtitleDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<SubtitleDto>> Handle(GetSubtitlesByStreetcodeIdQuery request, CancellationToken cancellationToken)
        {
            var subtitle = await _repositoryWrapper.SubtitleRepository
                .GetFirstOrDefaultAsync(Subtitle => Subtitle.StreetcodeId == request.StreetcodeId);

            NullResult<SubtitleDto> result = new NullResult<SubtitleDto>();
            result.WithValue(_mapper.Map<SubtitleDto>(subtitle));
            return result;
        }
    }
}
