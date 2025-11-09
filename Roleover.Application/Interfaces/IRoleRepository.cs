using Roleover.Domain.Entities;

namespace Roleover.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(string id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
    Task<IEnumerable<Role>> GetRolesByUserIdAsync(string userId);
}
