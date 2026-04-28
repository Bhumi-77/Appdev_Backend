using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VechicleSystem.Infrastructure.Data;

namespace VechicleSystem.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Database=VehiclePartsDB;Port=5432;User Id=postgres;Password=ajoob1234");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}