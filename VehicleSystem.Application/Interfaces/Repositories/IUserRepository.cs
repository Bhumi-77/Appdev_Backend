using VehicleSystem.Domain.Models;

namespace VehicleSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllStaffAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}