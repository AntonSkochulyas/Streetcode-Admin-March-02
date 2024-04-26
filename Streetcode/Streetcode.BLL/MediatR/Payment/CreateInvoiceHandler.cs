// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.DAL.Entities.Payment;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Payment
{
    /// <summary>
    /// Handler, that handles a process of creating a new invoice.
    /// </summary>
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Result<InvoiceInfo>>
    {
        // Currency code for Ukrainian Hryvnia.
        private const int _hryvnyaCurrencyCode = 980;

        // Currency multiplier used to calculate payment amount.
        private const int _currencyMultiplier = 100;

        // Payment service
        private readonly IPaymentService _paymentService;

        // Parametric constructor
        public CreateInvoiceHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Method, that creates a new invoice.
        /// </summary>
        /// <param name="request">
        /// Request with a new invoice.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Result, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<InvoiceInfo>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice(request.Payment.Amount * _currencyMultiplier, _hryvnyaCurrencyCode, new MerchantPaymentInfo { Destination = "Добровільний внесок на статутну діяльність ГО «Історична Платформа»" }, request.Payment.RedirectUrl);
            return Result.Ok(await _paymentService.CreateInvoiceAsync(invoice));
        }
    }
}