using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services; 
using VehicleSystem.Domain.Models;
using VechicleSystem.Infrastructure.Data;


namespace VechicleSystem.Infrastructure.Services
{
    public class VendorService : IVendorService
    {
        private readonly AppDbContext _context;

        // Dependency Injection
        public VendorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<VendorResponseDto>>> GetAllVendorsAsync()
        {
            // Query
            var vendors = await _context.VENDORs.ToListAsync(); 

            var result = vendors.Select(v => new VendorResponseDto
            {
                Id = v.Id,
                Name = v.Name,
                ContactPerson = v.ContactPerson,
                Phone = v.Phone,
                Email = v.Email,
                Address = v.Address,
                CreatedAt = v.CreatedAt
            }).ToList();

            return ApiResponse<List<VendorResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<VendorResponseDto>> GetVendorByIdAsync(int id)
        {
            var vendor = await _context.VENDORs.FindAsync(id); 

            if (vendor == null)
                return ApiResponse<VendorResponseDto>.Fail("Vendor not found");

            var result = new VendorResponseDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                ContactPerson = vendor.ContactPerson,
                Phone = vendor.Phone,
                Email = vendor.Email,
                Address = vendor.Address,
                CreatedAt = vendor.CreatedAt
            };

            return ApiResponse<VendorResponseDto>.Ok(result);
        }

        public async Task<ApiResponse<VendorResponseDto>> CreateVendorAsync(VendorRequestDto dto)
        {
            var vendor = new Vendor
            {
                Name = dto.Name,
                ContactPerson = dto.ContactPerson,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            // Inserting Data
            _context.VENDORs.Add(vendor); 
            await _context.SaveChangesAsync();

            var result = new VendorResponseDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                ContactPerson = vendor.ContactPerson,
                Phone = vendor.Phone,
                Email = vendor.Email,
                Address = vendor.Address,
                CreatedAt = vendor.CreatedAt
            };

            return ApiResponse<VendorResponseDto>.Ok(result, "Vendor created successfully");
        }

        public async Task<ApiResponse<VendorResponseDto>> UpdateVendorAsync(int id, VendorRequestDto dto)
        {
            var vendor = await _context.VENDORs.FindAsync(id); 

            if (vendor == null)
                return ApiResponse<VendorResponseDto>.Fail("Vendor not found");

            // Updating Data
            vendor.Name = dto.Name;
            vendor.ContactPerson = dto.ContactPerson;
            vendor.Phone = dto.Phone;
            vendor.Email = dto.Email;
            vendor.Address = dto.Address;

            await _context.SaveChangesAsync();

            var result = new VendorResponseDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                ContactPerson = vendor.ContactPerson,
                Phone = vendor.Phone,
                Email = vendor.Email,
                Address = vendor.Address,
                CreatedAt = vendor.CreatedAt
            };

            return ApiResponse<VendorResponseDto>.Ok(result, "Vendor updated successfully");
        }

        public async Task<ApiResponse<string>> DeleteVendorAsync(int id)
        {
            var vendor = await _context.VENDORs.FindAsync(id); 

            if (vendor == null)
                return ApiResponse<string>.Fail("Vendor not found");

            // Removing Data
            _context.VENDORs.Remove(vendor); 
            await _context.SaveChangesAsync();

            return ApiResponse<string>.Ok("Vendor deleted successfully");
        }
    }
}