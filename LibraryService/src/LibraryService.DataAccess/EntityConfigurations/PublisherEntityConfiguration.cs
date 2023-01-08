using LibrarySevice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySevice.DataAccess.EntityConfigurations
{
    public class PublisherEntityConfiguration : IEntityTypeConfiguration<PublisherEntity>
    {
        public void Configure(EntityTypeBuilder<PublisherEntity> builder)
        {
            builder.ToTable("Publisher");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.HasData
                (
                    new PublisherEntity() { Id = 1, Title = "Артек", Address = "ул. Пушкина дом Колотушкина"},
                    new PublisherEntity() { Id = 2, Title = "Книга", Address = "ул. Немига 44"}
                );
        }
    }
}
