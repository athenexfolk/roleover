using Roleover.Domain.Entities;

namespace Roleover.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task AddToRoleAsync(User user, Role role);
    Task RemoveFromRoleAsync(User user, Role role);
    Task GrantPermissionAsync(User user, Permission permission);
    Task RevokePermissionAsync(User user, Permission permission);
}
