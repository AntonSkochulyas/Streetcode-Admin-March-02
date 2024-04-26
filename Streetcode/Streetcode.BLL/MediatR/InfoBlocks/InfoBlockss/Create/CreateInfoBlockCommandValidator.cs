// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateInfoBlockCommand.
    /// </summary>
    public sealed class CreateInfoBlockCommandValidator : AbstractValidator<CreateInfoBlockCommand>
    {
        // Constructor
        public CreateInfoBlockCommandValidator()
        {
            RuleFor(command => command.NewInfoBlock.VideoURL)
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
