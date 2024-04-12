// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Email;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.AdditionalContent.Email;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Email
{
    /// <summary>
    /// Handler, that handles a process of sending email.
    /// </summary>
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, Result<Unit>>
    {
        // Email service
        private readonly IEmailService _emailService;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public SendEmailHandler(IEmailService emailService, ILoggerService logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Method, that sends an email.
        /// </summary>
        /// <param name="request">
        /// Request with email to send.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while sending process.
        /// </returns>
        public async Task<Result<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var message = new Message(new string[] { "streetcodeua@gmail.com" }, request.Email.From, "FeedBack", request.Email.Content);

            bool isResultSuccess = await _emailService.SendEmailAsync(message);

            if (isResultSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = EmailErrors.SendEmailHandlerFailedToSendEmailError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
