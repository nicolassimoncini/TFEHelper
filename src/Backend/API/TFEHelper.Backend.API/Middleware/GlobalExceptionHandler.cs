using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TFEHelper.Backend.Domain.Exceptions;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger; 
        
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) 
        { 
            _logger = logger; 
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                httpContext.Response.StatusCode = contextFeature.Error switch 
                { 
                    EntityNotFoundException => StatusCodes.Status404NotFound, 
                    _ => StatusCodes.Status500InternalServerError 
                };

                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                await httpContext.Response.WriteAsync(new APIResponseDTO()
                {
                    StatusCode = (HttpStatusCode)httpContext.Response.StatusCode,
                    ErrorMessages = new List<string>() { contextFeature.Error.Message },
                    IsSuccessful = false,
                    Payload = null
                }.ToString(),
                cancellationToken);
            }

            return true;
        }
    }
}
