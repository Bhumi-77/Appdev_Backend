using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IVendorService
    {
        Task<ApiResponse<List<VendorResponseDto>>> GetAllVendorsAsync();
        Task<ApiResponse<VendorResponseDto>> GetVendorByIdAsync(int id);
        Task<ApiResponse<VendorResponseDto>> CreateVendorAsync(VendorRequestDto dto);
        Task<ApiResponse<VendorResponseDto>> UpdateVendorAsync(int id, VendorRequestDto dto);
        Task<ApiResponse<string>> DeleteVendorAsync(int id);
    }
}