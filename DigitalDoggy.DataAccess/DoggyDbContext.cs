using DigitalDoggy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DigitalDoggy.DataAccess
{
    public class DoggyDbContext : DbContext
    {
        public DbSet<DogEntity> DogEntities { get; set; }

        public DoggyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}