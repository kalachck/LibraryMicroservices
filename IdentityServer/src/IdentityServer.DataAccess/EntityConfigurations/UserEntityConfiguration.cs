using IdentityServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.DataAccess.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar");

            builder.Property(x => x.UserName)
                .HasColumnName("Login")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar");

            builder.Property(u => u.PasswordHash)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnType("varchar");

            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnType("bit");

            builder.Ignore(x => x.NormalizedEmail);
            builder.Ignore(x => x.EmailConfirmed);
            builder.Ignore(x => x.NormalizedUserName);
            builder.Ignore(x => x.SecurityStamp);
            builder.Ignore(x => x.ConcurrencyStamp);
            builder.Ignore(x => x.PhoneNumber);
            builder.Ignore(x => x.PhoneNumberConfirmed);
            builder.Ignore(x => x.TwoFactorEnabled);
            builder.Ignore(x => x.LockoutEnabled);
            builder.Ignore(x => x.LockoutEnd);
            builder.Ignore(x => x.AccessFailedCount);
        }
    }
}
