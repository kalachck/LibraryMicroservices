using LibraryService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.DataAccess.EntityConfigurations
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.HasData
                (
                    new Genre() { Id = 1, Name = "Драма" },
                    new Genre() { Id = 2, Name = "Детектив" }
                );
        }
    }
}
