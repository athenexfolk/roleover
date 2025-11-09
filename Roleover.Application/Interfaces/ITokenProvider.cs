using Roleover.Domain.Entities;

namespace Roleover.Application.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(User user);
}
