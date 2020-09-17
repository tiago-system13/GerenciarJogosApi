using GerenciadorDeJogos.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace GerenciadorDeJogos.Api.Filtro
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NegocioException)
            {
                var statusCode = (int)HttpStatusCode.BadRequest;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = statusCode;

                if (context.Exception is NegocioException)
                {
                    var businessException = (NegocioException)context.Exception;

                    if (businessException?.Problema?.Erros.Count > 0)
                    {

                        context.Result = new JsonResult(new
                        {
                            error = businessException.Problema.Erros
                        });
                    }
                    else
                    {
                        context.Result = new JsonResult(new
                        {
                            error = businessException?.Message
                        });
                    }
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
