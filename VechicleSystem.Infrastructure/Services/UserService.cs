using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Domain.Models;

namespace VehicleSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<StaffResponseDto>> GetAllStaffAsync()
        {
            var users = await _userRepository.GetAllStaffAsync();

            return users.Select(u => new StaffResponseDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive
            }).ToList();
        }

        public async Task<StaffResponseDto?> GetStaffByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return null;

            return new StaffResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<StaffResponseDto> CreateStaffAsync(CreateStaffDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new StaffResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> UpdateStaffAsync(int id, UpdateStaffDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeactivateStaffAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return false;

            user.IsActive = false;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}