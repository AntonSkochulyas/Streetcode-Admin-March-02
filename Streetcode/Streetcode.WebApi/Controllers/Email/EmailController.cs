using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Email;
using Streetcode.BLL.MediatR.Email;

namespace Streetcode.WebApi.Controllers.Email
{
    /// <summary>
    /// Controller for handling email operations.
    /// </summary>
    public class EmailController : BaseApiController
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">The email data.</param>
        /// <returns>The result of the email sending operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] EmailDto email)
        {
            return HandleResult(await Mediator.Send(new SendEmailCommand(email)));
        }
    }
}
