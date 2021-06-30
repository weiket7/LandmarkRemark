using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LandmarkRemark.API.Utilities
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private static IWebHostEnvironment _env;
        private static ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            _logger.LogError(exception.StackTrace);

            var camelCaseSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var message = _env.IsProduction() ? "GeneralError" : exception.Message;
            var result = JsonConvert.SerializeObject(ApplicationResponse.Error(message),
                camelCaseSerializerSettings);

            context.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError; //500 if unexpected
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
