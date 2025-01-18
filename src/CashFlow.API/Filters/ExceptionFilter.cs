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
        if (context.Exception is CashFlowException ex)
        {
            context.HttpContext.Response.StatusCode = (int)ex.GetStatusError();
            context.Result = new ObjectResult(new ResponseErrorJson(ex.GetErrors));
        }else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourcesErrorsMessages.UNKNOWN_ERROR));
        }
    }
    
    private void ThrowUnkowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourcesErrorsMessages.UNKNOWN_ERROR));
    }
}