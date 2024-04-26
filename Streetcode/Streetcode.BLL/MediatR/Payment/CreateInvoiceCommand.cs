// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Payment;
using Streetcode.DAL.Entities.Payment;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Payment
{
    /// <summary>
    /// Command, that requests a handler to create a new invoice.
    /// </summary>
    /// <param name="newInvoice">
    /// New invoice.
    /// </param>
    public record CreateInvoiceCommand(PaymentDto Payment)
        : IRequest<Result<InvoiceInfo>>;
}