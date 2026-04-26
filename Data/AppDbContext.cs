using Microsoft.EntityFrameworkCore;
using Vehicle_System.Models;

namespace VehicleSystem.API.Data
{
    public class AppDbContext :DbContext

    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
