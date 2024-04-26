using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Payment;
using Streetcode.BLL.MediatR.Payment;

namespace Streetcode.WebApi.Controllers.Payment
{
    /// <summary>
    /// Controller for handling payment-related operations.
    /// </summary>
    public class PaymentController : BaseApiController
    {
        /// <summary>
        /// Creates an invoice for the specified payment.
        /// </summary>
        /// <param name="payment">The payment details.</param>
        /// <returns>The created invoice.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] PaymentDto payment)
        {
            return HandleResult(await Mediator.Send(new CreateInvoiceCommand(payment)));
        }
    }
}
