// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new partner.
    /// </summary>
    public class CreatePartnerHandler : IRequestHandler<CreatePartnerCommand, Result<PartnerDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreatePartnerHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new partner.
        /// </summary>
        /// <param name="request">
        /// Request with a new partner.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A PartnerDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<PartnerDto>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var newPartner = _mapper.Map<Partner>(request.newPartner);
            try
            {
                newPartner.Streetcodes.Clear();
                newPartner = await _repositoryWrapper.PartnersRepository.CreateAsync(newPartner);
                _repositoryWrapper.SaveChanges();
                var streetcodeIds = request.newPartner.Streetcodes.Select(s => s.Id).ToList();
                newPartner.Streetcodes.AddRange(await _repositoryWrapper
                    .StreetcodeRepository
                    .GetAllAsync(s => streetcodeIds.Contains(s.Id)));

                _repositoryWrapper.SaveChanges();
                return Result.Ok(_mapper.Map<PartnerDto>(newPartner));
            }
            catch(Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
