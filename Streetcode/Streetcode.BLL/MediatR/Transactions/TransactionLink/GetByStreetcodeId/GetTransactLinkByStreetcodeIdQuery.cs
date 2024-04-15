using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Transactions;

namespace Streetcode.BLL.MediatR.Transactions.TransactionLink.GetByStreetcodeId;

public record GetTransactLinkByStreetcodeIdQuery(int StreetcodeId)
    : IRequest<Result<TransactLinkDto?>>;