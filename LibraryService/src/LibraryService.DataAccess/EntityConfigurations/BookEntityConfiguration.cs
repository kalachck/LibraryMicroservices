using LibrarySevice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySevice.DataAccess.EntityConfigurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Book");

            builder.HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("nvarchar");

            builder.Property(x => x.PublicationDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.PageCount)
                .IsRequired()
                .HasColumnType("smallint");

            builder.HasData
                (
                    new BookEntity() 
                    { 
                        Id = 1, 
                        Title = "Война и мир", 
                        PublicationDate = DateTime.UtcNow.Date,
                        PageCount = 500, 
                        PublisherId = 1
                    },
                    new BookEntity()
                    {
                        Id = 2,
                        Title = "Борис Годунов",
                        PublicationDate = DateTime.UtcNow.Date,
                        PageCount = 300,
                        PublisherId = 2
                    }
                );
        }
    }
}
