using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update
{
    public class UpdateStreetcodeCategoryContentHandler : IRequestHandler<UpdateStreetcodeCategoryContentCommand, Result<StreetcodeCategoryContentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateStreetcodeCategoryContentHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<StreetcodeCategoryContentDto>> Handle(UpdateStreetcodeCategoryContentCommand request, CancellationToken cancellationToken)
        {
            var streetcodeCategoryContent = _mapper.Map<DAL.Entities.Sources.StreetcodeCategoryContent>(request.StreetcodeCategoryContentDto);

            if (streetcodeCategoryContent is null)
            {
                string errorMsg = SourceErrors.UpdateStreetcodeCategoryContentHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _repositoryWrapper.StreetcodeCategoryContentRepository.Update(streetcodeCategoryContent);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            var response = _mapper.Map<StreetcodeCategoryContentDto>(streetcodeCategoryContent);

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                string errorMsg = SourceErrors.UpdateStreetcodeCategoryContentHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
