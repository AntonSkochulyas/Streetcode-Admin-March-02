﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Streetcode.Fact;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.GetById;

public class GetFactByIdHandler : IRequestHandler<GetFactByIdQuery, Result<FactDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetFactByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<FactDto>> Handle(GetFactByIdQuery request, CancellationToken cancellationToken)
    {
        var facts = await _repositoryWrapper.FactRepository.GetItemBySpecAsync(new GetByIdFactSpec(request.Id));

        if (facts is null)
        {
            string errorMsg = string.Format(StreetcodeErrors.GetFactByIdCannotFindAnyFactWithCorrespondingIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<FactDto>(facts));
    }
}