using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FEMEE.Infrastructure.Data.Context
{
    public class FemeeDbContextFactory : IDesignTimeDbContextFactory<FemeeDbContext>
    {
        public FemeeDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("../FEMEE.API/appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<FemeeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new FemeeDbContext(optionsBuilder.Options);
        }
    }
}
