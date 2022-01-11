using DigitalDoggy.BusinessLogic.ApiCommands;
using DigitalDoggy.DataAccess;
using DigitalDoggy.Domain.Entities;
using DigitalDoggy.Tests.Comparers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DigitalDoggy.Tests
{
    public class CreateDogHandlerTests
    {
        private DoggyDbContext _doggyDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DoggyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _doggyDbContext = new DoggyDbContext(options);
        }

        [TestCase]
        public async Task SuccessCase()
        {
            var handler = new CreateDogCommandHandler(_doggyDbContext);
            var command = new CreateDogCommand
            {
                Name = "Doggy",
                Color = "red",
                TailLength = 100,
                Weight = 50
            };

            var response = await handler.Handle(command, default);
            Assert.IsTrue(response.Success);
            
            var createdDoggy = await _doggyDbContext.DogEntities.FindAsync(command.Name);
            var exceptedDoggy = new DogEntity
            {
                Name = "Doggy",
                Color = "red",
                TailLength = 100,
                Weight = 50
            };

            Assert.IsTrue(new DogEntityComparer().Equals(exceptedDoggy, createdDoggy));
        }

        [TestCase]
        public async Task FailedValidationCase()
        {
            var command = new CreateDogCommand
            {
                Name = string.Empty,
                Color = string.Empty,
                TailLength = -1,
                Weight = -1
            };

            var validator = new CreateDogCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            var exceptedErrorsCount = 4;

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(exceptedErrorsCount, validationResult.Errors.Count);
        }

        [TestCase]
        public async Task DuplicatedNameCase()
        {
            var handler = new CreateDogCommandHandler(_doggyDbContext);
            var command = new CreateDogCommand
            {
                Name = "Doggy",
                Color = "red",
                TailLength = 100,
                Weight = 50
            };

            await handler.Handle(command, default);
            var response = await handler.Handle(command, default);
            Assert.IsFalse(response.Success);
        }
    }
}