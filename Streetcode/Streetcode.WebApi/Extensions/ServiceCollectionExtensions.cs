using System.Reflection;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Email;
using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.BLL.Interfaces.Text;
using Streetcode.BLL.Services.Authentification;
using Streetcode.BLL.Services.Authorization;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.BLL.Services.Email;
using Streetcode.BLL.Services.Instagram;
using Streetcode.BLL.Services.Logging;
using Streetcode.BLL.Services.Payment;
using Streetcode.BLL.Services.Text;
using Streetcode.DAL.Entities.AdditionalContent.Email;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Authentication;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddRepositoryServices();
        services.AddFeatureManagement();
        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddAutoMapper(currentAssemblies);
        services.AddMediatR(currentAssemblies);

        //services.AddScoped<IBlobService, AzureBlobService>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IInstagramService, InstagramService>();
        services.AddScoped<ITextService, AddTermsToTextService>();
        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IRefreshTokenService, RefreshTokenService>();
    }

    public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        string connectionString;
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
        if (environment == "IntegrationTests" || environment == "Local")
        {
            var connection = configuration.GetSection(environment).GetConnectionString("DefaultConnection");
            connectionString = connection ?? throw new InvalidOperationException($"'DefaultConnection' is null or not found for the '{environment}' environment.");
        }
        else
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            connectionString = connection ?? throw new InvalidOperationException("'DefaultConnection' is null or not found");
        }

        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        if (emailConfig is not null)
        {
            services.AddSingleton(emailConfig);
        }

        services.AddDbContext<StreetcodeDbContext>(options =>
        {
            options.UseSqlServer(connectionString, opt =>
            {
                opt.MigrationsAssembly(typeof(StreetcodeDbContext).Assembly.GetName().Name);
                opt.MigrationsHistoryTable("__EFMigrationsHistory", schema: "entity_framework");
            });
        });

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(connectionString);
        });

        services.AddHangfireServer();

        var corsConfig = configuration.GetSection("CORS").Get<CorsConfiguration>();
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddHsts(opt =>
        {
            opt.Preload = true;
            opt.IncludeSubDomains = true;
            opt.MaxAge = TimeSpan.FromDays(30);
        });

        services.AddLogging();
        services.AddControllers();
    }

    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "StreetcodeApi", Version = "v1" });
            opt.CustomSchemaIds(x => x.FullName);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                     new OpenApiSecurityScheme
                     {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                     },
                     new string[] { }
                }
            });
        });
    }

    public class CorsConfiguration
    {
        public List<string>? AllowedOrigins { get; set; }
        public List<string>? AllowedHeaders { get; set; }
        public List<string>? AllowedMethods { get; set; }
        public int PreflightMaxAge { get; set; }
    }
}
