using Microsoft.EntityFrameworkCore;
using Vehicle_System.Models;

namespace VehicleSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        public DbSet<Part> Parts { get; set; } = null!;

        public DbSet<Invoice> Invoices { get; set; } = null!;

        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // InvoiceItem -> Invoice
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId);

            // Invoice -> Customer
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);

            // Invoice -> Staff(User)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Staff)
                .WithMany()
                .HasForeignKey(i => i.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}