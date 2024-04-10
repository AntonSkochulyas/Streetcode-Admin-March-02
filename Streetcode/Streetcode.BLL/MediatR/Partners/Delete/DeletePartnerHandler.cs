// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a partner.
    /// </summary>
    public class DeletePartnerHandler : IRequestHandler<DeletePartnerQuery, Result<PartnerDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeletePartnerHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes a partner.
        /// </summary>
        /// <param name="request">
        /// Request with partner id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A PartnerDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<PartnerDto>> Handle(DeletePartnerQuery request, CancellationToken cancellationToken)
        {
            var partner = await _repositoryWrapper.PartnersRepository.GetFirstOrDefaultAsync(p => p.Id == request.id);
            if (partner == null)
            {
                string errorMsg = PartnersErrors.DeletePartnerHandlerNoPartnerWithGivenIdError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }
            else
            {
                _repositoryWrapper.PartnersRepository.Delete(partner);
                try
                {
                    _repositoryWrapper.SaveChanges();
                    return Result.Ok(_mapper.Map<PartnerDto>(partner));
                }
                catch (Exception ex)
                {
                    _logger.LogError(request, ex.Message);
                    return Result.Fail(ex.Message);
                }
            }
        }
    }
}
