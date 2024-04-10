// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update
{
    /// <summary>
    /// Validator, that validates a model inside UpdateInfoBlockCommand.
    /// </summary>
    internal class UpdateInfoBlockCommandValidator : AbstractValidator<UpdateInfoBlockCommand>
    {
        // Constructor
        public UpdateInfoBlockCommandValidator()
        {
            RuleFor(command => command.infoBlock.VideoURL)
                .Custom((videoUrl, context) =>
                {
                    if (string.IsNullOrWhiteSpace(videoUrl) || !IsValidYouTubeURL(videoUrl))
                    {
                        context.AddFailure("Invalid link.");
                    }
                });
        }

        // Method, that checks is valid youtube url
        private static bool IsValidYouTubeURL(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return uri.Host.Equals("www.youtube.com", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
