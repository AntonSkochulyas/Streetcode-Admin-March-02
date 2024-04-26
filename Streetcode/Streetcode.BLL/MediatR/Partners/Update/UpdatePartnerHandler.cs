// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Update
{
    /// <summary>
    /// Handler, that handles a process of updating a partner.
    /// </summary>
    public class UpdatePartnerHandler : IRequestHandler<UpdatePartnerQuery, Result<PartnerDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public UpdatePartnerHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that updates a partner.
        /// </summary>
        /// <param name="request">
        /// Request with updated partner.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A PartnerDto, or error, if it was while updating process.
        /// </returns>
        public async Task<Result<PartnerDto>> Handle(UpdatePartnerQuery request, CancellationToken cancellationToken)
        {
            var partner = _mapper.Map<Partner>(request.Partner);

            try
            {
                var links = await _repositoryWrapper.PartnerSourceLinkRepository
                   .GetAllAsync(predicate: l => l.PartnerId == partner.Id);

                var newLinkIds = partner.PartnerSourceLinks.Select(l => l.Id).ToList();

                foreach (var link in links)
                {
                    if (!newLinkIds.Contains(link.Id))
                    {
                        _repositoryWrapper.PartnerSourceLinkRepository.Delete(link);
                    }
                }

                partner.Streetcodes.Clear();
                _repositoryWrapper.PartnersRepository.Update(partner);
                _repositoryWrapper.SaveChanges();
                var newStreetcodeIds = request.Partner.Streetcodes.Select(s => s.Id).ToList();
                var oldStreetcodes = await _repositoryWrapper.PartnerStreetcodeRepository
                    .GetAllAsync(ps => ps.PartnerId == partner.Id);

                foreach (var old in oldStreetcodes!)
                {
                    if (!newStreetcodeIds.Contains(old.StreetcodeId))
                    {
                        _repositoryWrapper.PartnerStreetcodeRepository.Delete(old);
                    }
                }

                foreach (var newCodeId in newStreetcodeIds!)
                {
                    if (oldStreetcodes.FirstOrDefault(x => x.StreetcodeId == newCodeId) == null)
                    {
                        _repositoryWrapper.PartnerStreetcodeRepository.Create(
                            new StreetcodePartner() { PartnerId = partner.Id, StreetcodeId = newCodeId });
                    }
                }

                _repositoryWrapper.SaveChanges();
                var dbo = _mapper.Map<PartnerDto>(partner);
                dbo.Streetcodes = request.Partner.Streetcodes;
                return Result.Ok(dbo);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
