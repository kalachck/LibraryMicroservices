using LibraryService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.DataAccess.EntityConfigurations
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.HasData
                (
                    new Author() { Id = 1, Name = "Лев"},
                    new Author() { Id = 2, Name = "Александр"}
                );
        }
    }
}
