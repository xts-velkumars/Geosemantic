using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Geosemantic.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<GeosemanticEntities>
    {
        public DatabaseContextFactory()
        {
        }

        public GeosemanticEntities CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GeosemanticEntities>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=config;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new GeosemanticEntities(builder.Options);
        }
    }
}
