using Dashboard.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result);

            return result.Code switch
            {
                var c when c.Contains("NOT_FOUND") || c.StartsWith("NO_") => NotFound(result),

                var c when c.Contains("UNAUTHORIZED") || c.Contains("CREDENTIALS") => Unauthorized(result),

                var c when c.Contains("DUPLICATE") || c.Contains("ALREADY_EXIST") => Conflict(result),

                _ => BadRequest(result)
            };
        }
    }
}
