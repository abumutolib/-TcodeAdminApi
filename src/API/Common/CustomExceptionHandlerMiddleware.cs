using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Application.Common.Exceptions;
using Infrastructure.Common.Exceptions;

namespace API.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            //(result, code) = exception switch
            //{
            //    ValidationException => ("", HttpStatusCode.BadRequest),
            //    NotFoundException => ("", HttpStatusCode.NotFound),
            //    UnauthorizedException => ("", HttpStatusCode.Unauthorized),
            //    _ => ("", HttpStatusCode.InternalServerError)
            //};

            switch (exception)
            {
                case ValidationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject((exception as ValidationException).Failures);
                    break;
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case UnauthorizedException:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
