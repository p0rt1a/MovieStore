using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();

            try
            {
                watch.Start();
                string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
                _logger.Write(message);

                await _next(context);
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {watch.ElapsedMilliseconds}ms";
                _logger.Write(message);
            }
            catch (Exception e)
            {
                watch.Stop();
                await HandleException(context, e, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception e, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = $"[Error] HTTP {context.Request.Method} - {context.Response.StatusCode}. Error Message: {e.Message} in {watch.ElapsedMilliseconds}ms";
            _logger.Write(message);

            var result = JsonConvert.SerializeObject(new { error = e.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
