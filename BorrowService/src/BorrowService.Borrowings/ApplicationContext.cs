using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Borrowings
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Borrowing> Borrowings { get; set; }

        public ApplicationContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=borrowingdatabase;Username=postgres;Password=secret");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BorrowingEntityConfiguration());
        }
    }
}
