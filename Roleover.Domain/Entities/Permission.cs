using Roleover.Domain.Entities.Common;

namespace Roleover.Domain.Entities;

public class Permission(string resource, string action, string? description = null) : IAuditable
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public string Resource { get; private set; } = resource;
    public string Action { get; private set; } = action;
    public string? Description { get; private set; } = description;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public void UpdateResource(string newResource)
    {
        Resource = newResource;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAction(string newAction)
    {
        Action = newAction;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string? newDescription)
    {
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"{Resource}:{Action}";
    }
}
