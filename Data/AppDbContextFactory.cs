using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VehicleSystem.API.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=vehicle_db;Username=postgres;Password=nabin");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
            