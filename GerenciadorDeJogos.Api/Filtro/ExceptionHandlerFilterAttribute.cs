using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace GerenciadorDeJogos.Api.Filtro
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                var statusCode = (int)HttpStatusCode.BadRequest;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = statusCode;

                if (context.Exception is ArgumentException)
                {
                    var businessException = (ArgumentException)context.Exception;

                    context.Result = new JsonResult(new
                    {
                        error = businessException?.Message
                    });
                }

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;

            context.Result = new JsonResult(new
            {
                error = context.Exception.Message,
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
