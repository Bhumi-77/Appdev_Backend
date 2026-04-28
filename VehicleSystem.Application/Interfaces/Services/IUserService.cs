using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<StaffResponseDto>> GetAllStaffAsync();
        Task<StaffResponseDto?> GetStaffByIdAsync(int id);
        Task<StaffResponseDto> CreateStaffAsync(CreateStaffDto dto);
        Task<StaffResponseDto?> UpdateStaffAsync(int id, UpdateStaffDto dto);
        Task<bool> DeleteStaffAsync(int id);
    }
}