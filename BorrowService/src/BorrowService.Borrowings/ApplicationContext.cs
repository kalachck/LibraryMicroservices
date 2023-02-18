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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BorrowingEntityConfiguration());
        }
    }
}
