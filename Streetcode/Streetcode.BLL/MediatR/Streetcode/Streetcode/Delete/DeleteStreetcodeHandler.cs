using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.BLL.Dto.Streetcode;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Delete;

public class DeleteStreetcodeHandler : IRequestHandler<DeleteStreetcodeCommand, Result<StreetcodeDto>>
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public DeleteStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<StreetcodeDto>> Handle(DeleteStreetcodeCommand request, CancellationToken cancellationToken)
    {
        var streetcode = await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.Id == request.Id);

        if (streetcode is null)
        {
            string errorMsg = $"The streetcode with id {request.Id} does not exist";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repository.StreetcodeRepository.Delete(streetcode);

        var resultIsSuccess = await _repository.SaveChangesAsync() > 0;
        var streetcodeDto = _mapper.Map<StreetcodeDto>(streetcode);
        if(resultIsSuccess && streetcodeDto != null)
        {
            return Result.Ok(streetcodeDto);
        }
        else
        {
            string errorMsg = StreetcodeErrors.DeleteRelatedTermHandlerFailedToDeleteRelatedTermError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
