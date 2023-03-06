using BorrowService.Borrowings.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowService.Borrowings.EntityConfigurations
{
    public class BorrowingEntityConfiguration : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            builder.ToTable("Borrowings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UserEmail)
                .IsRequired();

            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.AddingDate)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .IsRequired();
        }
    }
}
