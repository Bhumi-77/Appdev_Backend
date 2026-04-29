using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IPartService
    {
        Task<ApiResponse<List<PartResponseDto>>> GetAllPartsAsync();
        Task<ApiResponse<PartResponseDto>> GetPartByIdAsync(int id);
        Task<ApiResponse<PartResponseDto>> CreatePartAsync(PartRequestDto dto);
        Task<ApiResponse<PartResponseDto>> UpdatePartAsync(int id, PartRequestDto dto);
        Task<ApiResponse<string>> DeletePartAsync(int id);
        Task<ApiResponse<List<PartResponseDto>>> GetLowStockPartsAsync();
    }
}