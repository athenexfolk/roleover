using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;
using Roleover.Infrastructure.Data;

namespace Roleover.Infrastructure.Repositories;

public class PermissionRepository(ApplicationDbContext context) : IPermissionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Permission permission)
    {
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission permission)
    {
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await Task.FromResult(_context.Permissions.ToList());
    }

    public async Task<Permission?> GetByIdAsync(string id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    // public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(string roleId)
    // {
    //     var permissions = (from rp in _context.RolePermissions
    //                        join p in _context.Permissions on rp.PermissionId equals p.Id
    //                        where rp.RoleId == roleId
    //                        select p).ToList();
    //     return await Task.FromResult(permissions);
    // }

    // public async Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(string userId)
    // {
    //     var permissions = (from up in _context.UserPermissions
    //                        join p in _context.Permissions on up.PermissionId equals p.Id
    //                        where up.UserId == userId
    //                        select p).ToList();
    //     return await Task.FromResult(permissions);
    // }

    public async Task UpdateAsync(Permission permission)
    {
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync();
    }
}
