using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update
{
    internal class UpdateInfoBlockCommandValidator : AbstractValidator<UpdateInfoBlockCommand>
    {
        public UpdateInfoBlockCommandValidator()
        {
            RuleFor(command => command.InfoBlock.VideoURL)
                .Custom((videoUrl, context) =>
                {
                    if (string.IsNullOrWhiteSpace(videoUrl) || !IsValidYouTubeURL(videoUrl))
                    {
                        context.AddFailure("Invalid link.");
                    }
                });
        }

        private static bool IsValidYouTubeURL(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return uri.Host.Equals("www.youtube.com", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
