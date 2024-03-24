﻿using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Fact.ResetOrderNumbers;

public class ResetFactOrderNumbersHandler : IRequestHandler<ResetFactOrderNumbersCommand, Result<IEnumerable<FactDto>>>
{
    private const int Step = 100;

    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public ResetFactOrderNumbersHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<FactDto>>> Handle(ResetFactOrderNumbersCommand request, CancellationToken cancellationToken)
    {
        var facts =
            (await _repositoryWrapper.FactRepository
                .GetAllAsync(f => f.StreetcodeId == request.StreetcodeId))
            .OrderBy(f => f.OrderNumber == null)
            .ThenBy(f => f.OrderNumber);

        if (facts is null)
        {
            string errorMsg = $"Cannot find any fact by the streetcode id: {request.StreetcodeId}";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        if(!await ResetOrderNumbersToNull(facts))
        {
            const string errorMsg = $"Failed to reset order numbers";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        if (!await SetOrderNumbersSequantially(facts))
        {
            const string errorMsg = $"Failed to set order numbers";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var response = _mapper.Map<IEnumerable<FactDto>>(facts);
        return Result.Ok(response);
    }

    public async Task<bool> ResetOrderNumbersToNull(IOrderedEnumerable<DAL.Entities.Streetcode.TextContent.Fact> facts)
    {
        foreach (var fact in facts)
        {
            fact.OrderNumber = null;
            _repositoryWrapper.FactRepository.Update(fact);
        }

        return await _repositoryWrapper.SaveChangesAsync() > 0;
    }

    public async Task<bool> SetOrderNumbersSequantially(IOrderedEnumerable<DAL.Entities.Streetcode.TextContent.Fact> facts)
    {
        int orderNumber = Step;
        foreach (var fact in facts)
        {
            fact.OrderNumber = orderNumber;
            _repositoryWrapper.FactRepository.Update(fact);
            orderNumber += Step;
        }

        return await _repositoryWrapper.SaveChangesAsync() > 0;
    }
}
