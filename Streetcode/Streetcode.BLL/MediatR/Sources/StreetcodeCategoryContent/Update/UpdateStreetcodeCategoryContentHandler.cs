﻿using AutoMapper;
using FluentResults;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update
{
    public class UpdateStreetcodeCategoryContentHandler
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
                const string errorMsg = $"Cannot convert null to the street code category content";
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
                const string errorMsg = $"Failed to update the street code category content";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
