using BikeStore.Application.APIResponse;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeStore.Api.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
         
            var endpoint = httpContext.Request.Path;
            
            Dictionary<string, object> ErrorDetail = new Dictionary<string, object>();
            
            ApiResponse _response = new ApiResponse();
               

            if (exception is FluentValidation.ValidationException fluentException)
            {
                
                List<string> validationErrors = new List<string>();
                if (fluentException.Errors != null && fluentException.Errors.Any())
                {
                    foreach (var error in fluentException.Errors)
                    {
                        validationErrors.Add(error.ErrorMessage);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(fluentException.Message))
                {
                    validationErrors.Add(fluentException.Message);
                }


                ErrorDetail.Add("Instance", endpoint);
                ErrorDetail.Add("InnerException",exception.InnerException);
                _response.Metadata = ErrorDetail;
                _response.Message = "one or more validation errors occurred.";
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = validationErrors;
            }

            else
            { 
                ErrorDetail.Add("InnerException", exception.InnerException);
                ErrorDetail.Add("StackTrace", exception.StackTrace);
                ErrorDetail.Add("Instance", endpoint);
                _response.Message = exception.Message;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Metadata = ErrorDetail;
            }

            logger.LogError("{ProblemDetailsTitle}", _response.Message);

            _response.StatusCode =(HttpStatusCode)httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(_response, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
