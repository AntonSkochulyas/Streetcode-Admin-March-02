using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.GetAllByTermId
{
    public record GetAllRelatedTermsByTermIdHandler : IRequestHandler<GetAllRelatedTermsByTermIdQuery, Result<IEnumerable<RelatedTermDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerService _logger;

        public GetAllRelatedTermsByTermIdHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repository = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<RelatedTermDto>>> Handle(GetAllRelatedTermsByTermIdQuery request, CancellationToken cancellationToken)
        {
            var relatedTerms = await _repository.RelatedTermRepository
                .GetAllAsync(
                predicate: rt => rt.TermId == request.Id,
                include: rt => rt.Include(rt => rt.Term));

            if (relatedTerms is null)
            {
                string errorMsg = StreetcodeErrors.GetAllRelatedTermsByTermIdHandlerCannotGetWordsByTermIdError;
                _logger.LogError(request, errorMsg);
                return new Error(errorMsg);
            }

            var relatedTermsDTO = _mapper.Map<IEnumerable<RelatedTermDto>>(relatedTerms);

            if (relatedTermsDTO is null)
            {
                string errorMsg = StreetcodeErrors.GetAllRelatedTermsByTermIdHandlerCannotCreateDtosForRelatedWordsError;
                _logger.LogError(request, errorMsg);
                return new Error(errorMsg);
            }

            return Result.Ok(relatedTermsDTO);
        }
    }
}
