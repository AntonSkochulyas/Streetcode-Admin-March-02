using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.BLL.Services.Instagram;
using Streetcode.BLL.Services.Payment;

namespace Streetcode.WebApi.Extensions;

public static class ConfigureHostBuilderExtensions
{
    public static void ConfigureApplication(this ConfigureHostBuilder host)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

        host.ConfigureAppConfiguration((_, config) =>
        {
            config.ConfigureCustom(environment);
        });
    }

    public static void ConfigureBlob(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

        if (environment == "IntegrationTests")
        {
            services.Configure<BlobEnvironmentVariables>(builder.Configuration.GetSection(environment).GetSection("Blob"));
        }
        else
        {
            services.Configure<BlobEnvironmentVariables>(builder.Configuration.GetSection("Blob"));
        }
    }

    public static void ConfigurePayment(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<PaymentEnvironmentVariables>(builder.Configuration.GetSection("Payment"));
    }

    public static void ConfigureInstagram(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<InstagramEnvironmentVariables>(builder.Configuration.GetSection("Instagram"));
    }

    public static void ConfigureSerilog(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(builder.Configuration);
        });
    }

    public static void ConfigurePolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Default", new AuthorizationPolicyBuilder()
         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
         .RequireAuthenticatedUser()
         .Build());

            options.AddPolicy("Admin", new AuthorizationPolicyBuilder()
                .RequireRole("Admin")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        });
    }
}