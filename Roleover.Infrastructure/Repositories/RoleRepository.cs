using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;
using Roleover.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Roleover.Infrastructure.Repositories;

public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Role?> GetByIdAsync(string id) => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Role>> GetAllAsync() => await _context.Roles.ToListAsync();

    public async Task AddAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role role)
    {
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(string userId)
    {
        return await (from ur in _context.UserRoles
                      join r in _context.Roles on ur.RoleId equals r.Id
                      where ur.UserId == userId
                      select r).ToListAsync();
    }
}
