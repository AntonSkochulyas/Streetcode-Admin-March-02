using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Transactions;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Transactions.TransactionLink;

namespace Streetcode.BLL.MediatR.Transactions.TransactionLink.GetById;

public class GetTransactLinkByIdHandler : IRequestHandler<GetTransactLinkByIdQuery, Result<TransactLinkDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetTransactLinkByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<TransactLinkDto>> Handle(GetTransactLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var transactLink = await _repositoryWrapper.TransactLinksRepository
            .GetItemBySpecAsync(new GetByIdTransactionLinkSpec(request.Id));

        if (transactLink is null)
        {
            string errorMsg = string.Format(TransactionsErrors.GetTransactLinkByIdHandlerCanNotFindByIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<TransactLinkDto>(transactLink));
    }
}