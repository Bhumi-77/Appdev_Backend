using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<StaffResponseDto>> GetAllStaffAsync();
        Task<StaffResponseDto?> GetStaffByIdAsync(int id);
        Task<StaffResponseDto> CreateStaffAsync(CreateStaffDto dto);
        Task<bool> UpdateStaffAsync(int id, UpdateStaffDto dto);
        Task<bool> DeactivateStaffAsync(int id);
    }
}