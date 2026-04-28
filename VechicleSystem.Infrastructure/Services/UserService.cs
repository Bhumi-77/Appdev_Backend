using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Domain.Models;
using VehicleSystem.Infrastructure.Repositories;

namespace VehicleSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StaffResponseDto>> GetAllStaffAsync()
        {
            var users = await _repository.GetAllStaffAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<StaffResponseDto?> GetStaffByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return MapToDto(user);
        }

        public async Task<StaffResponseDto> CreateStaffAsync(CreateStaffDto dto)
        {
            var existingUser = await _repository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(user);
            return MapToDto(created);
        }

        public async Task<StaffResponseDto?> UpdateStaffAsync(int id, UpdateStaffDto dto)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            var updated = await _repository.UpdateAsync(user);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private StaffResponseDto MapToDto(User user)
        {
            return new StaffResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}