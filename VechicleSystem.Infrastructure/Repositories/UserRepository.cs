using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Domain.Models;
using VehicleSystem.Infrastructure.Data;

namespace VehicleSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllStaffAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Staff" || u.Role == "Admin")
                .OrderByDescending(u => u.Id)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}