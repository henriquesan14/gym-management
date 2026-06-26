using FluentValidation;
using GymManagementSystem.Shared.Common.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.ErrorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.AccessUnAuthorized => StatusCodes.Status401Unauthorized,
            ErrorType.AccessForbidden => StatusCodes.Status403Forbidden,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(
            statusCode: statusCode,
            title: error.Description,
            detail: $"{error.Code} - {error.Description}");
    }

    protected IActionResult? ValidateOrBadRequest<T>(
        T command,
        IValidator<T> validator)
    {
        var result = validator.Validate(command);
        if (!result.IsValid)
        {
            return BadRequest(new
            {
                statusCode = 400,
                message = result.Errors.Select(e => e.ErrorMessage).ToList()
            });
        }
        return null;
    }
}