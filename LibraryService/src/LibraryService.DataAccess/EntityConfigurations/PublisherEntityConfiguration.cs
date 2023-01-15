using LibrarySevice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySevice.DataAccess.EntityConfigurations
{
    public class PublisherEntityConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.HasData
                (
                    new Publisher() { Id = 1, Name = "Артек", Address = "ул. Пушкина дом Колотушкина"},
                    new Publisher() { Id = 2, Name = "Книга", Address = "ул. Немига 44"}
                );
        }
    }
}
