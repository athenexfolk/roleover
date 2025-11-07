using Microsoft.EntityFrameworkCore;
using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;
using Roleover.Infrastructure.Data;

namespace Roleover.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();

    public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(string id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByUsernameAsync(string username) => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
