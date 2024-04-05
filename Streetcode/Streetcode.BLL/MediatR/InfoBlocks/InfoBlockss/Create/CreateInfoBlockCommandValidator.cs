using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    public sealed class CreateInfoBlockCommandValidator : AbstractValidator<CreateInfoBlockCommand>
    {
        public CreateInfoBlockCommandValidator()
        {
            RuleFor(command => command.newInfoBlock.VideoURL)
                .Custom((videoUrl, context) =>
                {
                    if (string.IsNullOrWhiteSpace(videoUrl) || !IsValidYouTubeURL(videoUrl))
                    {
                        context.AddFailure("Invalid link.");
                    }
                });
        }

        private bool IsValidYouTubeURL(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return uri.Host.Equals("www.youtube.com", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
