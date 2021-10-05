using GameStore.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.API.Filters
{
  public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
  {
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger _logger;

    public ApiExceptionFilterAttribute(ILogger logger)
    {
      _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
      {
        { typeof(ValidationException), HandleValidationException },
        { typeof(NotFoundException), HandleNotFoundException },
      };
      _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
      HandleException(context);

      base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
      Type type = context.Exception.GetType();
      if(_exceptionHandlers.ContainsKey(type))
      {
        _exceptionHandlers[type].Invoke(context);
        return;
      }

      if(!context.ModelState.IsValid)
      {
        HandleInvalidModelStateException(context);
        return;
      }

      HandleUnknownException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
      var exception = context.Exception as ValidationException;

      var details = new ValidationProblemDetails(exception.Errors)
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };

      context.Result = new BadRequestObjectResult(details);

      context.ExceptionHandled = true;

      _logger.Error(context.Exception, "Validation Exception Occured!");
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
      var details = new ValidationProblemDetails(context.ModelState)
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };

      context.Result = new BadRequestObjectResult(details);

      context.ExceptionHandled = true;

      _logger.Error(context.Exception, "Invalid Model State Exception Occured!");

    }

    private void HandleNotFoundException(ExceptionContext context)
    {
      var exception = context.Exception as NotFoundException;

      var details = new ProblemDetails()
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        Title = "The specified resource was not found.",
        Detail = exception.Message
      };

      context.Result = new NotFoundObjectResult(details);

      context.ExceptionHandled = true;

      _logger.Error(exception, "Not Found Exception Occured!");

    }

    private void HandleUnknownException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Title = "An error occurred while processing your request.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        Detail = context.Exception.Message
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status500InternalServerError
      };

      context.ExceptionHandled = true;

      _logger.Error(context.Exception, "Unknown Exception Occured!");
    }
  }
}
