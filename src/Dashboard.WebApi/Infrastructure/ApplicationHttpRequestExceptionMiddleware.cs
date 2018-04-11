using System;
using System.Net;
using System.Threading.Tasks;
using Dashboard.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dashboard.WebApi.Infrastructure
{
    public class ApplicationHttpRequestExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApplicationHttpRequestExceptionMiddleware> _logger;

        public ApplicationHttpRequestExceptionMiddleware(RequestDelegate next, ILogger<ApplicationHttpRequestExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationHttpRequestException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                context.Response.ContentType = "application/json";

                var responseData = JsonConvert.SerializeObject(new
                {
                    Message = "ApplicationHttpRequestExceptionMiddleware caught exception",
                    ExceptionName = "ApplicationHttpRequestException",
                    Response = new
                    {
                        StatusCode = ex.Response.StatusCode,
                        StatusDescription = ex.Response.StatusDescription,
                        Content = ex.Response.Content,
                        IsSuccessful = ex.Response.IsSuccessful,
                        ResponseStatus = ex.Response.ResponseStatus,
                    }
                });
                _logger.LogWarning($"ApplicationHttpRequestException: {responseData}");

                await context.Response.WriteAsync(responseData);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApplicationHttpRequestExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationHttpRequestExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationHttpRequestExceptionMiddleware>();
        }
    }
}
