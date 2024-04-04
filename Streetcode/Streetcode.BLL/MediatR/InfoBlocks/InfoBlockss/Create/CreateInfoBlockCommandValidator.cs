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
                    if (string.IsNullOrWhiteSpace(videoUrl) || !IsValidYouTubeUrl(videoUrl))
                    {
                        context.AddFailure("Invalid link.");
                    }
                });
        }

        private bool IsValidYouTubeUrl(string url)
        {
            Uri? uriResult;
            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                              && uriResult.Host == "www.youtube.com";

            return isValidUrl;
        }
    }
}
