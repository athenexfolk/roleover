using Roleover.Domain.Entities;

namespace Roleover.Application.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(string id);
    Task<IEnumerable<Permission>> GetAllAsync();
    Task AddAsync(Permission permission);
    Task UpdateAsync(Permission permission);
    Task DeleteAsync(Permission permission);
}
