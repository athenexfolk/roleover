using Roleover.Domain.Entities.Common;

namespace Roleover.Domain.Entities;

public class Role(string name) : IAuditable
{
    public string Id { get; } = Guid.NewGuid().ToString();
    
    public string Name { get; private set; } = name;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public void UpdateName(string newName)
    {
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }
}
