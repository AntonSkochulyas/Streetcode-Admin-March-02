using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Streetcode.RelatedFigure;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.GetByTagId
{
    internal class GetRelatedFiguresByTagIdHandler : IRequestHandler<GetRelatedFiguresByTagIdQuery, Result<IEnumerable<RelatedFigureDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetRelatedFiguresByTagIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<RelatedFigureDto>>> Handle(GetRelatedFiguresByTagIdQuery request, CancellationToken cancellationToken)
        {
            var streetcodes = await _repositoryWrapper.StreetcodeRepository
               .GetItemsBySpecAsync(new GetAllBytagIdRelatedFigureSpec(request.TagId));

            if (streetcodes is null)
            {
                string errorMsg = string.Format(StreetcodeErrors.GetRelatedFigureByTagIdHandlerCannotFindAnyStreetcodeWithcorrespondingTagIdError, request.TagId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<RelatedFigureDto>>(streetcodes));
        }
    }
}
