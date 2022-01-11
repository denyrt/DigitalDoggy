using DigitalDoggy.DataAccess;
using DigitalDoggy.Domain.Constants;
using DigitalDoggy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDoggy.BusinessLogic.ApiCommands
{
    public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, CreateDogResponse>
    {
        private readonly DoggyDbContext _doggyDbContext;

        public CreateDogCommandHandler(DoggyDbContext doggyDbContext)
        {
            _doggyDbContext = doggyDbContext;
        }

        public async Task<CreateDogResponse> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            var keyValues = new object[] { request.Name };
            if (await _doggyDbContext.DogEntities.FindAsync(keyValues, cancellationToken) != null)
            {
                return CreateDogResponse.FromError(ResponseMessageCodes.Conflict);
            }

            var dogEntity = new DogEntity
            {
                Name = request.Name,
                Color = request.Color,
                TailLength = request.TailLength,
                Weight = request.Weight
            };

            try
            {
                var create = await _doggyDbContext.DogEntities.AddAsync(dogEntity, cancellationToken);
                await _doggyDbContext.SaveChangesAsync(cancellationToken);
                return CreateDogResponse.SuccessResponse;
            }
            catch (DbUpdateException ex) // log 'ex' somewhere.
            {
                return CreateDogResponse.FromError(ResponseMessageCodes.Conflict);
            }
        }
    }
}
