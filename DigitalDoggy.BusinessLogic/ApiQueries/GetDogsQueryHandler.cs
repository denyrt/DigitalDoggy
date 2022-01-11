using DigitalDoggy.DataAccess;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DigitalDoggy.BusinessLogic.Extensions;
using Microsoft.EntityFrameworkCore;
using DigitalDoggy.BusinessLogic.Models;
using DigitalDoggy.Domain.Constants;

namespace DigitalDoggy.BusinessLogic.ApiQueries
{
    public class GetDogsQueryHandler : IRequestHandler<GetDogsQuery, GetDogsResponse>
    {
        private readonly DoggyDbContext _doggyDbContext;

        public GetDogsQueryHandler(DoggyDbContext doggyDbContext)
        {
            _doggyDbContext = doggyDbContext;
        }

        public async Task<GetDogsResponse> Handle(GetDogsQuery request, CancellationToken cancellationToken)
        {
            var data = await _doggyDbContext.DogEntities
                .SortBy(request, x => x.Name)
                .Paginate(request)
                .Select(x => new Dog 
                {
                    Name = x.Name,
                    Color = x.Color,
                    TailLength = x.TailLength,
                    Weight = x.Weight
                })
                .ToArrayAsync(cancellationToken);

            return new GetDogsResponse
            {
                Success = true,
                Message = ResponseMessageCodes.Success,
                Dogs = data
            };
        }
    }
}
