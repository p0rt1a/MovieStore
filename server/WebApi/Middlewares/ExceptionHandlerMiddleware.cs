using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = Stopwatch.StartNew();

            try
            {
                string message = $"[Request]HTTP {context.Request.Method} to {context.Request.Path}";
                _loggerService.Write(message);

                await _next.Invoke(context);
                
                watch.Stop();
                message = $"[Response]HTTP {context.Request.Method} responded with {context.Response.StatusCode} in {watch.Elapsed.Milliseconds} ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(watch, ex, context);
            }
        }

        private Task HandleException(Stopwatch watch, Exception ex, HttpContext context)
        {
            string message = $"[Error]HTTP {context.Request.Method} to {context.Request.Path} caused an error. Error: {ex.Message} in {watch.Elapsed.Milliseconds} ms";

            _loggerService.Write(message);
        
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandleMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}