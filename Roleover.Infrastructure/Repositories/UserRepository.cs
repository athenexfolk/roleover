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

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var query = _context.Users.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(u => u.Username)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(string id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByUsernameAsync(string username) => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task AddToRoleAsync(User user, Role role)
    {
        var userRole = new UserRole(user.Id, role.Id);
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromRoleAsync(User user, Role role)
    {
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
    }

    public async Task GrantPermissionAsync(User user, Permission permission)
    {
        var userPermission = new UserPermission(user.Id, permission.Id);
        _context.UserPermissions.Add(userPermission);
        await _context.SaveChangesAsync();
    }

    public async Task RevokePermissionAsync(User user, Permission permission)
    {
        var userPermission = await _context.UserPermissions.FirstOrDefaultAsync(up => up.UserId == user.Id && up.PermissionId == permission.Id);
        if (userPermission != null)
        {
            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();
        }
    }
}