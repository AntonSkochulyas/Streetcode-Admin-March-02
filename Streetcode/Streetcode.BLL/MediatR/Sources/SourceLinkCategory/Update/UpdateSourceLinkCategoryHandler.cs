﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public class UpdateSourceLinkCategoryHandler : IRequestHandler<UpdateSourceLinkCategoryCommand, Result<SourceLinkCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateSourceLinkCategoryHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<SourceLinkCategoryDto>> Handle(UpdateSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            var sourceLinkCategory = _mapper.Map<DAL.Entities.Sources.SourceLinkCategory>(request.SourceLinkDto);

            if (sourceLinkCategory is null)
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            if (sourceLinkCategory.Title is null)
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryCommandValidatorTitleIsRequiredError;
                _logger.LogError(sourceLinkCategory, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var image = await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(
                x => x.Id == sourceLinkCategory.ImageId);

            if (image is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.UpdateSourceLinkHandlerCanNotFindImageWithGivenIdError,
                    request.SourceLinkDto.ImageId);

                _logger.LogError(sourceLinkCategory, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.SourceCategoryRepository.Update(sourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            var response = _mapper.Map<SourceLinkCategoryDto>(sourceLinkCategory);

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
