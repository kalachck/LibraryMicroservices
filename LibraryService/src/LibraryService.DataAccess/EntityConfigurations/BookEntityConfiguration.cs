using LibrarySevice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySevice.DataAccess.EntityConfigurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.GenreId);

            builder.HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AuthorId)
                .IsRequired(false);

            builder.Property(x => x.GenreId)
                .IsRequired(false);

            builder.Property(x => x.PublisherId)
                .IsRequired(false);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.Property(x => x.PublicationDate)
                .IsRequired()
                .HasColumnType("date");

            builder.HasData
                (
                    new Book()
                    {
                        Id = 1,
                        Title = "Война и мир",
                        PublicationDate = DateTime.UtcNow.Date,
                        AuthorId = 1,
                        GenreId = 1,
                        PublisherId = 1
                    },
                    new Book()
                    {
                        Id = 2,
                        Title = "Борис Годунов",
                        AuthorId = 2,
                        GenreId = 2,
                        PublicationDate = DateTime.UtcNow.Date,
                        PublisherId = 2
                    }
                );
        }
    }
}
