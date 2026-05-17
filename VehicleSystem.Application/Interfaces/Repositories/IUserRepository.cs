using VehicleSystem.Domain.Models;

namespace VehicleSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllStaffAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}