// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Handler, that creates a tag.
    /// </summary>
    public class CreateTagHandler : IRequestHandler<CreateTagCommand, Result<TagDto>>
    {
        // Mappper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateTagHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creating a tag.
        /// </summary>
        /// <param name="request">
        /// Request with tag inside to create.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token to cancelling operation if it needed.
        /// </param>
        /// <returns>
        /// A TagDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var newTag = await _repositoryWrapper.TagRepository.CreateAsync(new DAL.Entities.AdditionalContent.Tag()
            {
                Title = request.Tag.Title
            });

            try
            {
                _repositoryWrapper.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.ToString());
                return Result.Fail(ex.ToString());
            }

            return Result.Ok(_mapper.Map<TagDto>(newTag));
        }
    }
}
