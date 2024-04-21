using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Toponyms;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.GetById;

public class GetToponymByIdHandler : IRequestHandler<GetToponymByIdQuery, Result<ToponymDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetToponymByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ToponymDto>> Handle(GetToponymByIdQuery request, CancellationToken cancellationToken)
    {
        var toponym = await _repositoryWrapper.ToponymRepository
           .GetItemBySpecAsync(new GetByIdToponymSpec(request.Id));

        if (toponym is null)
        {
            string errorMsg = string.Format(ToponymsErrors.GetToponymByIdHandlerCanNotFindByIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<ToponymDto>(toponym));
    }
}