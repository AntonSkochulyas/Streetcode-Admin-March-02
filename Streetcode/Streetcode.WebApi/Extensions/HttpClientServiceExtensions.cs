using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.BLL.Services.Instagram;
using Streetcode.BLL.Services.Payment;

namespace Streetcode.WebApi.Extensions
{
    public static class HttpClientServiceExtensions
    {
        public static void AddCustomHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IInstagramService, InstagramService>();
            services.AddHttpClient<IPaymentService, PaymentService>();
        }
    }
}
