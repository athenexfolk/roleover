using Roleover.Application.Exceptions;
using Roleover.Application.Interfaces;

namespace Roleover.Infrastructure.Utilities;

public class AuthProvider(IUserRepository userRepository, ITokenProvider tokenProvider, IPasswordHasher passwordHasher, IRoleRepository roleRepository) : IAuthProvider
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username) ?? throw new UnauthorizedException("Invalid credentials");

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
            throw new UnauthorizedException("Invalid credentials");

        var roles = await _roleRepository.GetRolesByUserIdAsync(user.Id);
        foreach (var role in roles) user.AddRole(role);

        return _tokenProvider.GenerateToken(user);
    }
}
