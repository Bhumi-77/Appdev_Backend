using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleSystem.Domain.Models;

namespace VechicleSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ERD Table
        public DbSet<Vendor> VENDORs { get; set; }
        public DbSet<Part> PARTs { get; set; }
        public DbSet<PurchaseInvoice> PURCHASE_INVOICEs { get; set; }
        public DbSet<PurchaseInvoiceItem> PURCHASE_INVOICE_ITEMs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            // Vendor Mapping
            modelBuilder.Entity<Vendor>(e =>
            {
                e.ToTable("VENDOR"); 
                e.HasKey(v => v.Id);
                e.Property(v => v.Name).IsRequired();
                e.Property(v => v.Phone);
                e.Property(v => v.Email);
                e.Property(v => v.Address);
                e.Property(v => v.ContactPerson);
            });

            // Parts Mapping
            modelBuilder.Entity<Part>(e =>
            {
                e.ToTable("PART");
                e.HasKey(p => p.Id);
                e.Property(p => p.SellingPrice).HasColumnType("decimal(18,2)");
                e.Property(p => p.CostPrice).HasColumnType("decimal(18,2)");
                e.Property(p => p.StockQuantity).IsRequired();
                e.Property(p => p.LowStockThreshold).IsRequired();

                e.HasOne(p => p.Vendor)
                 .WithMany(v => v.Parts)
                 .HasForeignKey(p => p.VendorId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // PURCHASE_INVOICE Mapping
            modelBuilder.Entity<PurchaseInvoice>(e =>
            {
                e.ToTable("PURCHASE_INVOICE");
                e.HasKey(p => p.Id);
                e.Property(p => p.AdminId).IsRequired();

                e.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
                e.Property(p => p.PurchaseDate).IsRequired();

                e.HasOne(p => p.Vendor)
                 .WithMany(v => v.PurchaseInvoices)
                 .HasForeignKey(p => p.VendorId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // PURCHASE_INVOICE_ITEM Mapping
            modelBuilder.Entity<PurchaseInvoiceItem>(e =>
            {
                e.ToTable("PURCHASE_INVOICE_ITEM");
                e.HasKey(p => p.Id);
                e.Property(p => p.CostPrice).HasColumnType("decimal(18,2)");
                e.Property(p => p.LineTotal).HasColumnType("decimal(18,2)");

                e.HasOne(p => p.PurchaseInvoice)
                 .WithMany(pi => pi.Items)
                 .HasForeignKey(p => p.PurchaseInvoiceId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(p => p.Part)
                 .WithMany()
                 .HasForeignKey(p => p.PartId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}