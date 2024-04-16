using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete;

public class DeleteFactHandler : IRequestHandler<DeleteFactCommand, Result<FactDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public DeleteFactHandler(
        IRepositoryWrapper repositoryWrapper,
        ILoggerService logger,
        IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<FactDto>> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
    {
        int id = request.Id;
        var fact = await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(n => n.Id == id);
        if (fact == null)
        {
            string errorMsg = string.Format(StreetcodeErrors.DeleteFactHandlerNoFactFoundByEnteredIdError, id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(errorMsg);
        }

        _repositoryWrapper.FactRepository.Delete(fact);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (resultIsSuccess)
        {
            return Result.Ok(_mapper.Map<FactDto>(fact));
        }
        else
        {
            string errorMsg = StreetcodeErrors.DeleteFactHandlerFailedToDeleteError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}