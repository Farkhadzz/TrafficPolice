using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
using TrafficPoliceApp.Repositories.Base;
using TrafficPoliceApp.Repositories;
using TrafficPoliceApp.Models;


namespace TrafficPoliceApp.Middlewares;
public class LoggingMiddleware : IMiddleware
{
    private readonly ILoggerRepository loggerRepository;
    private readonly IDataProtector dataProtector;
    public LoggingMiddleware(ILoggerRepository loggerRepository, IDataProtectionProvider dataProtectionProvider)
    {
        this.loggerRepository = loggerRepository;
        this.dataProtector = dataProtectionProvider.CreateProtector("Context");
    }
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        if (!loggerRepository.IsLoggingEnabled())
        {
            await next.Invoke(httpContext);
        }
        else
        {
            var methodType = httpContext.Request.Method;

            var url = httpContext.Request.GetDisplayUrl();

            var userId = httpContext.Request.Cookies["Authorize"] is null ? default : Convert.ToInt16(dataProtector.Unprotect(httpContext.Request.Cookies["Authorize"]));

            var requestBody = string.Empty;

            if (httpContext.Request.Body.CanRead)
            {
                if (!httpContext.Request.Body.CanSeek)
                {
                    httpContext.Request.EnableBuffering();
                }

                httpContext.Request.Body.Position = 0;
                StreamReader requestReader = new(httpContext.Request.Body, Encoding.UTF8);
                requestBody = await requestReader.ReadToEndAsync();
                httpContext.Request.Body.Position = 0;
            }

            var responseBody = string.Empty;

            Stream originalBody = httpContext.Response.Body;

            using (var memStream = new MemoryStream())
            {
                httpContext.Response.Body = memStream;
                await next.Invoke(httpContext);
                memStream.Position = 0;
                StreamReader responseReader = new(httpContext.Response.Body, Encoding.UTF8);
                responseBody = await responseReader.ReadToEndAsync();
                memStream.Position = 0;

                await memStream.CopyToAsync(originalBody);
            }

            var statusCode = httpContext.Response.StatusCode;
            httpContext.Response.Body = originalBody;
            await this.loggerRepository.Logging(new Models.Logging
            {
                UserId = userId,
                Url = url,
                MethodType = methodType,
                StatusCode = statusCode,
                RequestBody = requestBody,
                ResponseBody = responseBody
            });
        }
    }
}