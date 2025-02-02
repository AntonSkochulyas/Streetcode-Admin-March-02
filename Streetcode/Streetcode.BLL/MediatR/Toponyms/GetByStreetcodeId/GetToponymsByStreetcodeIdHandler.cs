using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Toponyms;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.GetByStreetcodeId;

public class GetToponymsByStreetcodeIdHandler : IRequestHandler<GetToponymsByStreetcodeIdQuery, Result<IEnumerable<ToponymDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetToponymsByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ToponymDto>>> Handle(GetToponymsByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        var toponyms = await _repositoryWrapper
            .ToponymRepository
            .GetItemsBySpecAsync(new GetByStreetcodeIdToponymSpec(request.StreetcodeId));

        if (toponyms is null)
        {
            string errorMsg = string.Format(ToponymsErrors.GetToponymsByStreetcodeIdHandlerCanNotFindByStreetcodeIdError, request.StreetcodeId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var uniqueToponyms = toponyms.DistinctBy(x => x.StreetName);

        var toponymDto = uniqueToponyms.GroupBy(x => x.StreetName).Select(group => group.First()).Select(x => _mapper.Map<ToponymDto>(x));
        return Result.Ok(toponymDto);
    }
}
