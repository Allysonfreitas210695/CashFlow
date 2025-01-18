using System.Globalization;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
            HandleProjectException(context);
        else
            ThrowUnkowError(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            var errors = (ErrorOnValidationException)context.Exception;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(errors.Errors));
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
    }
    
    private void ThrowUnkowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourcesErrorsMessages.UNKNOWN_ERROR));
    }
}