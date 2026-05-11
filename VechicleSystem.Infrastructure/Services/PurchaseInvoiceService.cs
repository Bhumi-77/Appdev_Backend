using Microsoft.EntityFrameworkCore;
using VechicleSystem.Infrastructure.Data;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Domain.Models;

namespace VechicleSystem.Infrastructure.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly AppDbContext _context;

        public PurchaseInvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<PurchaseInvoiceResponseDto>>> GetAllAsync()
        {
            var invoices = await _context.PURCHASE_INVOICEs
                .Include(i => i.Vendor)
                .Include(i => i.Items)
                    .ThenInclude(item => item.Part)
                .ToListAsync();

            var result = invoices.Select(i => new PurchaseInvoiceResponseDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                VendorId = i.VendorId,
                VendorName = i.Vendor.Name,
                AdminId = i.AdminId,
                TotalAmount = i.TotalAmount,
                PurchaseDate = i.PurchaseDate,
                Notes = i.Notes,
                Items = i.Items.Select(item => new PurchaseInvoiceItemResponseDto
                {
                    Id = item.Id,
                    PartId = item.PartId,
                    PartName = item.Part.Name,
                    Quantity = item.Quantity,
                    CostPrice = item.CostPrice,
                    LineTotal = item.LineTotal
                }).ToList()
            }).ToList();

            return ApiResponse<List<PurchaseInvoiceResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<PurchaseInvoiceResponseDto>> GetByIdAsync(int id)
        {
            var invoice = await _context.PURCHASE_INVOICEs
                .Include(i => i.Vendor)
                .Include(i => i.Items)
                    .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                return ApiResponse<PurchaseInvoiceResponseDto>.Fail("Invoice not found");

            var result = new PurchaseInvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                VendorId = invoice.VendorId,
                VendorName = invoice.Vendor.Name,
                AdminId = invoice.AdminId,
                TotalAmount = invoice.TotalAmount,
                PurchaseDate = invoice.PurchaseDate,
                Notes = invoice.Notes,
                Items = invoice.Items.Select(item => new PurchaseInvoiceItemResponseDto
                {
                    Id = item.Id,
                    PartId = item.PartId,
                    PartName = item.Part.Name,
                    Quantity = item.Quantity,
                    CostPrice = item.CostPrice,
                    LineTotal = item.LineTotal
                }).ToList()
            };

            return ApiResponse<PurchaseInvoiceResponseDto>.Ok(result);
        }

        public async Task<ApiResponse<PurchaseInvoiceResponseDto>> CreateAsync(PurchaseInvoiceRequestDto dto)
        {
            // Check if the vendor exists
            var vendor = await _context.VENDORs.FindAsync(dto.VendorId);
            if (vendor == null)
                return ApiResponse<PurchaseInvoiceResponseDto>.Fail("Vendor not found");

            // Validate all parts exist beforehand
            foreach (var itemDto in dto.Items)
            {
                var partExists = await _context.PARTs.FindAsync(itemDto.PartId);
                if (partExists == null)
                    return ApiResponse<PurchaseInvoiceResponseDto>.Fail($"Part with ID {itemDto.PartId} not found");
            }

            // Auto-generating invoice numbers
            var invoiceNumber = $"PI-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

            // Build invoice items & calculate totals
            var invoiceItems = new List<PurchaseInvoiceItem>();
            decimal totalAmount = 0;

            foreach (var itemDto in dto.Items)
            {
                var lineTotal = itemDto.Quantity * itemDto.CostPrice;
                totalAmount += lineTotal;

                invoiceItems.Add(new PurchaseInvoiceItem
                {
                    PartId = itemDto.PartId,
                    Quantity = itemDto.Quantity,
                    CostPrice = itemDto.CostPrice,
                    LineTotal = lineTotal
                });
            }

            // Creating invoices
            var invoice = new PurchaseInvoice
            {
                VendorId = dto.VendorId,
                AdminId = dto.AdminId,
                InvoiceNumber = invoiceNumber,
                TotalAmount = totalAmount,  // auto-calculated
                PurchaseDate = dto.PurchaseDate,
                Notes = dto.Notes,
                Items = invoiceItems
            };

            _context.PURCHASE_INVOICEs.Add(invoice);

            // Auto-updating stock quantity for each part
            foreach (var itemDto in dto.Items)
            {
                var part = await _context.PARTs.FindAsync(itemDto.PartId);
                if (part != null)
                {
                    part.StockQuantity += itemDto.Quantity;  // increase stock
                    part.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            // Reloading with navigation properties for the response
            var created = await _context.PURCHASE_INVOICEs
                .Include(i => i.Vendor)
                .Include(i => i.Items)
                    .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == invoice.Id);

            var result = new PurchaseInvoiceResponseDto
            {
                Id = created!.Id,
                InvoiceNumber = created.InvoiceNumber,
                VendorId = created.VendorId,
                VendorName = created.Vendor.Name,
                AdminId = created.AdminId,
                TotalAmount = created.TotalAmount,
                PurchaseDate = created.PurchaseDate,
                Notes = created.Notes,
                Items = created.Items.Select(item => new PurchaseInvoiceItemResponseDto
                {
                    Id = item.Id,
                    PartId = item.PartId,
                    PartName = item.Part.Name,
                    Quantity = item.Quantity,
                    CostPrice = item.CostPrice,
                    LineTotal = item.LineTotal
                }).ToList()
            };

            return ApiResponse<PurchaseInvoiceResponseDto>.Ok(result, "Purchase invoice created successfully. Stock updated.");
        }
    }
}