using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Data.Contexts;

namespace Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProjectManagementDB;Trusted_Connection=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
