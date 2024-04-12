// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Email;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Email;

/// <summary>
/// Command, that request handler to send an email.
/// </summary>
/// <param name="Email">
/// Email to send.
/// </param>
public record SendEmailCommand(EmailDto Email)
    : IRequest<Result<Unit>>;
