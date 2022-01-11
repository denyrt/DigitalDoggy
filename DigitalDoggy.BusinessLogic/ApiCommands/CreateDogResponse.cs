using DigitalDoggy.BusinessLogic.Responses;

namespace DigitalDoggy.BusinessLogic.ApiCommands
{
    public record CreateDogResponse : ResponseBase<CreateDogResponse>
    {
        public static CreateDogResponse FromError(string message) => new()
        {
            Message = message,
            Success = false
        };
    }
}