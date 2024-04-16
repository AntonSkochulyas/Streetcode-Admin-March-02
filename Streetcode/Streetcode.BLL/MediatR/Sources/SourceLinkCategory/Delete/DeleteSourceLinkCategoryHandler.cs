using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete
{
    public class DeleteSourceLinkCategoryHandler : IRequestHandler<DeleteSourceLinkCategoryCommand, Result<SourceLinkCategoryDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public DeleteSourceLinkCategoryHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<SourceLinkCategoryDto>> Handle(DeleteSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            var sourceLinkCategory = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(sc => sc.Id == id);
            if (sourceLinkCategory == null)
            {
                string errorMsg = string.Format(SourceErrors.DeleteSourceLinkCategoryHandlerCanNotFindSourceLinkCategoryWithGivenIdError, id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.SourceCategoryRepository.Delete(sourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<SourceLinkCategoryDto>(sourceLinkCategory));
            }
            else
            {
                string errorMsg = SourceErrors.DeleteSourceLinkCategoryHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
