﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    public class CreateRelatedTermHandler : IRequestHandler<CreateRelatedTermCommand, Result<RelatedTermDto>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public CreateRelatedTermHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<RelatedTermDto>> Handle(CreateRelatedTermCommand request, CancellationToken cancellationToken)
        {
            var relatedTerm = _mapper.Map<Entity>(request.RelatedTerm);

            if (relatedTerm is null)
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotCreateNewRelatedWordForTermError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var existingTerms = await _repository.RelatedTermRepository
                .GetAllAsync(
                predicate: rt => rt.TermId == request.RelatedTerm.TermId && rt.Word == request.RelatedTerm.Word);

            if (existingTerms is null || existingTerms.Any())
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerWordWithThisDefinitionAlreadyExistsError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var createdRelatedTerm = _repository.RelatedTermRepository.Create(relatedTerm);

            var isSuccessResult = await _repository.SaveChangesAsync() > 0;

            if(!isSuccessResult)
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotSaveChangesInDatabaseAfterRelatedWordCreationError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var createdRelatedTermDTO = _mapper.Map<RelatedTermDto>(createdRelatedTerm);

            if(createdRelatedTermDTO != null)
            {
                return Result.Ok(createdRelatedTermDTO);
            }
            else
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotMapEntityError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
