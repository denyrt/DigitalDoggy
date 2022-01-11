using DigitalDoggy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalDoggy.DataAccess.Configurations
{
    public class DogEntityConfiguration : IEntityTypeConfiguration<DogEntity>
    {
        public void Configure(EntityTypeBuilder<DogEntity> builder)
        {
            builder.HasKey(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.Color).HasMaxLength(255);
        }
    }
}