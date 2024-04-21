// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Transactions;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Transactions.TransactionLink;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Transactions.TransactionLink.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all transactions from database.
    /// </summary>
    public class GetAllTransactLinksHandler : IRequestHandler<GetAllTransactLinksQuery, Result<IEnumerable<TransactLinkDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllTransactLinksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all transactions from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all transactions from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of TransactLinkDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<TransactLinkDto>>> Handle(GetAllTransactLinksQuery request, CancellationToken cancellationToken)
        {
            var transactLinks = await _repositoryWrapper.TransactLinksRepository.GetItemsBySpecAsync(new GetAllTransactionLinkSpec());

            if (transactLinks is null)
            {
                string errorMsg = TransactionsErrors.GetAllTransactLinksHandlerCanNotFindAnyLinkError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<TransactLinkDto>>(transactLinks));
        }
    }
}
