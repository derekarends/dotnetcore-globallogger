using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Middleware
{
  public static class ExceptionMiddlewareExtensions
  {
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory logger)
    {
      app.UseExceptionHandler(appError =>
      {
        appError.Run(async context =>
        {
          context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
          context.Response.ContentType = "application/json";

          var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
          if (contextFeature != null)
          {
            logger.CreateLogger("GlobalException")
              .LogError($"Something went wrong: {contextFeature.Error}");

            await context.Response.WriteAsync(new ErrorResult
            {
              StatusCode = context.Response.StatusCode,
              Message = "Internal Server Error."
            }.ToString());
          }
        });
      });
    }
  }
}