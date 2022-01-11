namespace DigitalDoggy.BusinessLogic.Responses
{
    public record ErrorResponse : ResponseBase<ErrorResponse>
    {
        public string Description { get; init; }
    
        public static ErrorResponse Create(string message, string description)
        {
            return new()
            {
                Success = false,
                Message = message,
                Description = description
            };
        }
    }
}