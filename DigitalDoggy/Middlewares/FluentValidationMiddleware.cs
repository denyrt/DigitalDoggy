using DigitalDoggy.BusinessLogic.Responses;
using DigitalDoggy.Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDoggy.Middlewares
{
    public class FluentValidationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                var description = string.Join(Environment.NewLine, ex.Errors.Select(x => x.ErrorMessage));
                var response = ErrorResponse.Create(ResponseMessageCodes.ValidationError, description);
                context.Response.StatusCode = 400; // Or use 422, but i prefer use 400
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
