using Roleover.Domain.Entities.Common;

namespace Roleover.Domain.Entities;

public class User(string username, string email, string passwordHash) : IAuditable
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public string Username { get; } = username;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    private readonly List<Role> _roles = [];
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRole(Role role)
    {
        if (_roles.Any(r => r.Id == role.Id)) return;
        _roles.Add(role);
    }
}
