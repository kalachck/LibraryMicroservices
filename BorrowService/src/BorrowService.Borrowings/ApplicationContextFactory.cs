using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BorrowService.Borrowings
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("MigrationConfiguration.json")
                .Build(); 
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("BorrowConnection"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
