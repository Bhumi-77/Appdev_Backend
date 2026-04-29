using Microsoft.EntityFrameworkCore;
using VechicleSystem.Infrastructure.Data;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Domain.Models;

namespace VechicleSystem.Infrastructure.Services
{
    public class PartService : IPartService
    {
        private readonly AppDbContext _context;

        public PartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<PartResponseDto>>> GetAllPartsAsync()
        {
            // Include Vendor so to return VendorName in response
            var parts = await _context.PARTs
                .Include(p => p.Vendor)
                .ToListAsync();

            var result = parts.Select(p => new PartResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                StockQuantity = p.StockQuantity,
                LowStockThreshold = p.LowStockThreshold,
                VendorId = p.VendorId,
                VendorName = p.Vendor.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return ApiResponse<List<PartResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<PartResponseDto>> GetPartByIdAsync(int id)
        {
            var part = await _context.PARTs
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (part == null)
                return ApiResponse<PartResponseDto>.Fail("Part not found");

            var result = new PartResponseDto
            {
                Id = part.Id,
                Name = part.Name,
                Category = part.Category,
                Description = part.Description,
                SellingPrice = part.SellingPrice,
                CostPrice = part.CostPrice,
                StockQuantity = part.StockQuantity,
                LowStockThreshold = part.LowStockThreshold,
                VendorId = part.VendorId,
                VendorName = part.Vendor.Name,
                CreatedAt = part.CreatedAt,
                UpdatedAt = part.UpdatedAt
            };

            return ApiResponse<PartResponseDto>.Ok(result);
        }

        public async Task<ApiResponse<PartResponseDto>> CreatePartAsync(PartRequestDto dto)
        {
            // Checking vendor exists first
            var vendor = await _context.VENDORs.FindAsync(dto.VendorId);
            if (vendor == null)
                return ApiResponse<PartResponseDto>.Fail("Vendor not found");

            var part = new Part
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                SellingPrice = dto.SellingPrice,
                CostPrice = dto.CostPrice,
                StockQuantity = dto.StockQuantity,
                LowStockThreshold = dto.LowStockThreshold,
                VendorId = dto.VendorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.PARTs.Add(part);
            await _context.SaveChangesAsync();

            var result = new PartResponseDto
            {
                Id = part.Id,
                Name = part.Name,
                Category = part.Category,
                Description = part.Description,
                SellingPrice = part.SellingPrice,
                CostPrice = part.CostPrice,
                StockQuantity = part.StockQuantity,
                LowStockThreshold = part.LowStockThreshold,
                VendorId = part.VendorId,
                VendorName = vendor.Name,
                CreatedAt = part.CreatedAt,
                UpdatedAt = part.UpdatedAt
            };

            return ApiResponse<PartResponseDto>.Ok(result, "Part created successfully");
        }

        public async Task<ApiResponse<PartResponseDto>> UpdatePartAsync(int id, PartRequestDto dto)
        {
            var part = await _context.PARTs
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (part == null)
                return ApiResponse<PartResponseDto>.Fail("Part not found");

            var vendor = await _context.VENDORs.FindAsync(dto.VendorId);
            if (vendor == null)
                return ApiResponse<PartResponseDto>.Fail("Vendor not found");

            // Updating all fields
            part.Name = dto.Name;
            part.Category = dto.Category;
            part.Description = dto.Description;
            part.SellingPrice = dto.SellingPrice;
            part.CostPrice = dto.CostPrice;
            part.StockQuantity = dto.StockQuantity;
            part.LowStockThreshold = dto.LowStockThreshold;
            part.VendorId = dto.VendorId;
            part.UpdatedAt = DateTime.UtcNow; // update timestamp

            await _context.SaveChangesAsync();

            var result = new PartResponseDto
            {
                Id = part.Id,
                Name = part.Name,
                Category = part.Category,
                Description = part.Description,
                SellingPrice = part.SellingPrice,
                CostPrice = part.CostPrice,
                StockQuantity = part.StockQuantity,
                LowStockThreshold = part.LowStockThreshold,
                VendorId = part.VendorId,
                VendorName = vendor.Name,
                CreatedAt = part.CreatedAt,
                UpdatedAt = part.UpdatedAt
            };

            return ApiResponse<PartResponseDto>.Ok(result, "Part updated successfully");
        }

        public async Task<ApiResponse<string>> DeletePartAsync(int id)
        {
            var part = await _context.PARTs.FindAsync(id);
            if (part == null)
                return ApiResponse<string>.Fail("Part not found");

            _context.PARTs.Remove(part);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.Ok("Part deleted successfully");
        }

        // for low stock alerts
        public async Task<ApiResponse<List<PartResponseDto>>> GetLowStockPartsAsync()
        {
            var parts = await _context.PARTs
                .Include(p => p.Vendor)
                .Where(p => p.StockQuantity < p.LowStockThreshold)
                .ToListAsync();

            var result = parts.Select(p => new PartResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                StockQuantity = p.StockQuantity,
                LowStockThreshold = p.LowStockThreshold,
                VendorId = p.VendorId,
                VendorName = p.Vendor.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return ApiResponse<List<PartResponseDto>>.Ok(result);
        }
    }
}