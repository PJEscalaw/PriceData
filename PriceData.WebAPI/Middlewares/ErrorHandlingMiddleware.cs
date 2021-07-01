using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceData.WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private string ReturnBadRequestResponse(ResponseException error)
        {
            var errorResponse = JsonSerializer.Serialize(new
            {
                error.StatusCode,
                error.Succeeded,
                error.Message,
                error.Errors,
                error.Data
            });

            return errorResponse;
        }


        private string ReturnInternalServerResponse(Exception error)
        {

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            var logError = JsonSerializer.Serialize(new
            {
                ErrorMessage = error.Message,
                StackTrace = error.StackTrace,
                Source = error.Source,
                InnerException = (error.InnerException == null ? "" : error.InnerException.Message),
                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            });

            Log.Error(logError, "[SERVER ERROR]");

            var errorResponse = JsonSerializer.Serialize(new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "The API has encountered an Internal Server Error. Please try again.",
                ErrorDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            });

            return errorResponse;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case ResponseException e:
                    context.Response.StatusCode = e.StatusCode;
                    await context.Response.WriteAsync(ReturnBadRequestResponse(e));
                    break;
                case Exception e:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(ReturnInternalServerResponse(e));
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(ReturnInternalServerResponse(ex));
                    break;
            }
        }
    }
}
