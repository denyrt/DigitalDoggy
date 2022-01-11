using DigitalDoggy.Domain.Constants;
using System.ComponentModel;

namespace DigitalDoggy.BusinessLogic.Responses
{
    public record ResponseBase
    {
        [DefaultValue("Success")]
        public string Message { get; init; }
        
        [DefaultValue(true)]
        public bool Success { get; init; }
    }

    public abstract record ResponseBase<T> : ResponseBase where T : ResponseBase, new()
    {
        public static T SuccessResponse => new()
        {
            Message = ResponseMessageCodes.Success,
            Success = true,
        };
    }
}
