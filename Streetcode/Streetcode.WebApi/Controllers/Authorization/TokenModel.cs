namespace Streetcode.WebApi.Controllers.Auth
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
