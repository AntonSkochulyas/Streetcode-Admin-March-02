using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Streetcode.WebApi.ExceptionHandlers
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = Guid.NewGuid();
            LogInfoAboutError(traceId, exception.Message, exception.StackTrace);

            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case ValidationException validationException:
                    {
                        WriteInfoAboutErrorToProblemDetails(
                            problemDetails,
                            "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                            "Validation error",
                            StatusCodes.Status400BadRequest,
                            context.Request.Path,
                            $"One or more validation errors has occured, traceID: {traceId}");

                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        if (validationException.Errors is not null)
                        {
                            problemDetails.Extensions["errors"] = validationException.Errors;
                        }

                        break;
                    }

                default:
                    {
                        WriteInfoAboutErrorToProblemDetails(
                            problemDetails,
                            "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                            "Internal Server Error",
                            StatusCodes.Status500InternalServerError,
                            context.Request.Path,
                            $"Internal server error occured, traceID: {traceId}");

                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        break;
                    }
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        private void LogInfoAboutError(Guid traceId, string message, string? stackTrace)
        {
            _logger.LogError($"Error occure while processing the request, TraceID: {traceId}, Message: {message}, StackTrace: {stackTrace}");
        }

        private static void WriteInfoAboutErrorToProblemDetails(ProblemDetails problemDetails, string errorType, string errorTitle, int errorStatusCode, string errorInstance, string errorDetail)
        {
            problemDetails.Type = errorType;
            problemDetails.Title = errorTitle;
            problemDetails.Status = errorStatusCode;
            problemDetails.Instance = errorInstance;
            problemDetails.Detail = errorDetail;
        }
    }
}
