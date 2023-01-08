using LibrarySevice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySevice.DataAccess.EntityConfigurations
{
    public class BookAuthorEntityConfiguration : IEntityTypeConfiguration<BookAuthorEntity>
    {
        public void Configure(EntityTypeBuilder<BookAuthorEntity> builder)
        {
            builder.ToTable("BookAuthor");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.BookId);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.AuthorId);

            builder.HasData
                (
                    new BookAuthorEntity() { Id = 1, BookId = 1, AuthorId = 1},
                    new BookAuthorEntity() { Id = 2, BookId = 2, AuthorId = 2}
                );
        }
    }
}
