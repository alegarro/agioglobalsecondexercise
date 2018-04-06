using PruebaUnoAgioGlobal.Core.Exceptions;
using PruebaUnoAgioGlobal.Web.Exceptions.Entities;
using PruebaUnoAgioGlobal.Web.Exceptions.Handlers.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.Exceptions.Handlers
{
    internal class DefaultExceptionHandler : IExceptionHandler
    {
        public DefaultExceptionHandler(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment)
        {
            Logger = loggerFactory.CreateLogger<DefaultExceptionHandler>();
            IncludeStackTrace = hostingEnvironment.IsDevelopment();
        }

        private bool IncludeStackTrace { get; }

        private ILogger Logger { get; }

        public virtual async Task Handle(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            Logger.LogCritical(default(EventId), ex, ex?.Message);

            var statusCode = HttpStatusCode.InternalServerError;
            var message = string.Empty;

            switch (ex)
            {
                case ValidationException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case NotFoundException e:
                    statusCode = HttpStatusCode.NotFound;
                    message = e.Message;
                    break;
                case ConflictException _:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                case ForbiddenException _:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // TODO: Put definitives domains in production environment!!
            context.Response.Headers.Add("Access-Control-Allow-Origin", new StringValues("*"));

            var serializedException = string.Empty;
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                    serializedException = JsonConvert.SerializeObject(new InternalServerExceptionInfo { Code = default(EventId).Id.ToString(), Message = ex.Message, StackTrace = IncludeStackTrace ? ex.StackTrace : "NOT AVAILABLE" });
                    break;
                case HttpStatusCode.BadRequest:
                    serializedException = JsonConvert.SerializeObject(new BadRequestExceptionInfo { Code = default(EventId).Id.ToString(), Reasons = ((ValidationException)ex).Errors.Select(e => e.ErrorMessage).ToList() });
                    break;
                case HttpStatusCode.Unauthorized:
                    serializedException = JsonConvert.SerializeObject(new UnauthorizedExceptionInfo { Code = default(EventId).Id.ToString(), Message = "UNAUTHORIZED" });
                    break;
                case HttpStatusCode.NotFound:
                    serializedException = JsonConvert.SerializeObject(new NotFoundExceptionInfo { Code = default(EventId).Id.ToString(), Message = "NOT FOUND: " + message});
                    break;
                case HttpStatusCode.Conflict:
                    serializedException = JsonConvert.SerializeObject(new ConflictExceptionInfo { Code = default(EventId).Id.ToString(), Message = ex.Message });
                    break;
                case HttpStatusCode.Forbidden:
                    serializedException = JsonConvert.SerializeObject(new ForbiddenExceptionInfo { Code = default(EventId).Id.ToString(), Message = "FORBIDDEN" });
                    break;
            }         

            await context.Response.WriteAsync(serializedException).ConfigureAwait(false);
        }
    }
}
