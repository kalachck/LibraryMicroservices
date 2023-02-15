using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityConfiguration());
        }
    }
}
