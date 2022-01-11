using DigitalDoggy.BusinessLogic.Models;
using DigitalDoggy.BusinessLogic.Responses;

namespace DigitalDoggy.BusinessLogic.ApiQueries
{
    public record GetDogsResponse : ResponseBase<GetDogsResponse>
    {
        public Dog[] Dogs { get; init; }
    }
}