using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Geosemantic.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<SteEntities>
    {
        public DatabaseContextFactory()
        {
        }

        public SteEntities CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SteEntities>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=config;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new SteEntities(builder.Options);
        }
    }
}
