// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Transactions;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Transactions.TransactionLink.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all transactions from database.
    /// </summary>
    public record GetAllTransactLinksQuery : IRequest<Result<IEnumerable<TransactLinkDto>>>
    {
        public GetAllTransactLinksQuery()
        {
        }
    }
}