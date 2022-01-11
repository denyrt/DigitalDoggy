using DigitalDoggy.BusinessLogic.Responses;
using DigitalDoggy.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDoggy.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(this ResponseBase response)
        {
            return response.Message switch
            {
                ResponseMessageCodes.Conflict => new ConflictObjectResult(response),
                ResponseMessageCodes.Success => new OkObjectResult(response),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}