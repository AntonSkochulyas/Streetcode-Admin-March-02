﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create;

public class CreateFactHandler : IRequestHandler<CreateFactCommand, Result<FactDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public CreateFactHandler(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<FactDto>> Handle(CreateFactCommand request, CancellationToken cancellationToken)
    {
        if (await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.StreetcodeId) is null)
        {
            return LogAndReturnError($"The streetcode with id {request.Fact.StreetcodeId} does not exist", request);
        }

        if (await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.ImageId) is null)
        {
            return LogAndReturnError($"The image with id {request.Fact.ImageId} does not exist", request);
        }

        var fact = _mapper.Map<DAL.Entities.Streetcode.TextContent.Fact>(request.Fact);

        if (fact is null)
        {
            return LogAndReturnError($"Cannot convert null to fact", request);
        }

        _repositoryWrapper.FactRepository.Create(fact);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (resultIsSuccess)
        {
            var factDto = _mapper.Map<FactDto>(fact);
            return Result.Ok(factDto);
        }
        else
        {
            return LogAndReturnError($"Failed to create a fact", request);
        }
    }

    private Result<FactDto> LogAndReturnError(string errorMsg, CreateFactCommand request)
    {
        _logger.LogError(request, errorMsg);
        return Result.Fail(new Error(errorMsg));
    }
}