﻿using Microsoft.AspNetCore.Diagnostics;
using FoodBlog.Application.Common.Exceptions;

namespace FoodBlog.WebApi.Filters;

public class KnownExceptionsHandler : IExceptionHandler
{
    private static readonly IDictionary<Type, Func<HttpContext, Exception, IResult>> ExceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, IResult>>
    {
        { typeof(ValidationException), HandleValidationException },
        { typeof(NotFoundException), HandleNotFoundException }
    };

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Type type = exception.GetType();

        if (ExceptionHandlers.TryGetValue(type, out var handler))
        {
            IResult result = handler.Invoke(httpContext, exception);
            await result.ExecuteAsync(httpContext);
            return true;
        }

        return false;
    }

    private static IResult HandleValidationException(HttpContext context, Exception exception)
    {
        var validationException = exception as ValidationException ?? throw new InvalidOperationException("Exception is not of type ValidationException");

        return Results.ValidationProblem(validationException.Errors,
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1");
    }

    private static IResult HandleNotFoundException(HttpContext context, Exception exception) =>
        Results.Problem(statusCode: StatusCodes.Status404NotFound,
            title: "The specified resource was not found.",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            detail: exception.Message);
}