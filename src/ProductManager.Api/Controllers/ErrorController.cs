using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Domain.Commons.Exceptions;

namespace ProductManager.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ErrorController()
        {
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context.Error;

            if(exception is BusinessException)
            {
                BusinessException ex = (BusinessException) exception; //casting

                return BadRequest(new {message = ex.Message, Errors = ex.Validations });
            }

            if (exception.InnerException is BusinessException)
            {
                BusinessException ex = (BusinessException)exception.InnerException; //casting

                return BadRequest(new { message = ex.Message, Errors = ex.Validations });
            }

            throw exception;

        }
    }
}

