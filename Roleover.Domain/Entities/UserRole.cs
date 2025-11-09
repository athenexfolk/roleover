namespace Roleover.Domain.Entities;

public class UserRole(string userId, string roleId)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    
    public string UserId { get; } = userId;
    public string RoleId { get; } = roleId;
}
