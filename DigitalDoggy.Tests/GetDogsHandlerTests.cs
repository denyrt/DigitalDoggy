using DigitalDoggy.BusinessLogic.ApiQueries;
using DigitalDoggy.BusinessLogic.Models;
using DigitalDoggy.DataAccess;
using DigitalDoggy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDoggy.Tests
{
    public class GetDogsHandlerTests
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
        public async Task SortingCase()
        {
            await SeedSortingCase();
            var handler = new GetDogsQueryHandler(_doggyDbContext);
            var query = new GetDogsQuery
            {
                Order = "desc",
                PropertyName = "tailLength"
            };
            var result = await handler.Handle(query, default);
            var actual = result.Dogs;
            var exceptedResult = new Dog[]
            {
                new()
                {
                    Name = "Doggy 2",
                    Color = "(12, 12, 12)",
                    TailLength = 2,
                    Weight = 2
                },
                new()
                {
                    Name = "Doggy 1",
                    Color = "(11, 11, 11)",
                    TailLength = 1,
                    Weight = 1
                }
            };

            Assert.IsTrue(Enumerable.SequenceEqual(exceptedResult, actual));
        }

        [TestCase]
        public async Task PaginationCase()
        {
            await SeedPaginationCase();
            var handler = new GetDogsQueryHandler(_doggyDbContext);
            var query = new GetDogsQuery
            {
                PageNumber = 1,
                PageSize = 2
            };
            var result = await handler.Handle(query, default);
            var actual = result.Dogs;
            var exceptedResult = new Dog[]
            {
                new()
                {
                    Name = "Doggy 3",
                    Color = "(13, 13, 13)",
                    TailLength = 3,
                    Weight = 3
                },
                new()
                {
                    Name = "Doggy 4",
                    Color = "(14, 14, 14)",
                    TailLength = 4,
                    Weight = 4
                }
            };

            Assert.IsTrue(Enumerable.SequenceEqual(exceptedResult, actual));
        }

        [TestCase]
        public async Task SortingAndPaginationCase()
        {
            await SeedSortingAndPagination();
            var handler = new GetDogsQueryHandler(_doggyDbContext);
            var query = new GetDogsQuery
            {
                Order = "asc",
                PropertyName = "weight",
                PageNumber = 0,
                PageSize = 2
            };
            var result = await handler.Handle(query, default);
            var actual = result.Dogs;
            var exceptedResult = new Dog[]
            {
                new()
                {
                    Name = "Doggy",
                    Color = "red",
                    TailLength = 17,
                    Weight = 1
                },
                new()
                {
                    Name = "Cache",
                    Color = "pink",
                    TailLength = 24,
                    Weight = 9
                }
            };

            Assert.IsTrue(Enumerable.SequenceEqual(exceptedResult, actual));
        }

        private async Task SeedSortingCase()
        {
            await _doggyDbContext.DogEntities.AddRangeAsync(new DogEntity[]
            {
                new()
                {
                    Name = "Doggy 1",
                    Color = "(11, 11, 11)",
                    TailLength = 1,
                    Weight = 1
                },
                new()
                {
                    Name = "Doggy 2",
                    Color = "(12, 12, 12)",
                    TailLength = 2,
                    Weight = 2
                }
            });
            await _doggyDbContext.SaveChangesAsync();
        }

        private async Task SeedPaginationCase()
        {
            await _doggyDbContext.DogEntities.AddRangeAsync(new DogEntity[]
            {
                new()
                {
                    Name = "Doggy 1",
                    Color = "(11, 11, 11)",
                    TailLength = 1,
                    Weight = 1
                },
                new()
                {
                    Name = "Doggy 2",
                    Color = "(12, 12, 12)",
                    TailLength = 2,
                    Weight = 2
                },
                new()
                {
                    Name = "Doggy 3",
                    Color = "(13, 13, 13)",
                    TailLength = 3,
                    Weight = 3
                },
                new()
                {
                    Name = "Doggy 4",
                    Color = "(14, 14, 14)",
                    TailLength = 4,
                    Weight = 4
                }
            });
            await _doggyDbContext.SaveChangesAsync();
        }

        private async Task SeedSortingAndPagination()
        {
            await _doggyDbContext.DogEntities.AddRangeAsync(new DogEntity[]
            {
                new()
                {
                    Name = "Cache",
                    Color = "pink",
                    TailLength = 24,
                    Weight = 9
                },
                new()
                {
                    Name = "Doggy",
                    Color = "red",
                    TailLength = 17,
                    Weight = 1
                },
                new()
                {
                    Name = "Hash",
                    Color = "white",
                    TailLength = 133,
                    Weight = 53
                },
                new()
                {
                    Name = "Core",
                    Color = "transparent",
                    TailLength = 13,
                    Weight = 42
                }
            });
            await _doggyDbContext.SaveChangesAsync();
        }
    }
}
