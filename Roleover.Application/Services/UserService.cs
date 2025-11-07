using Roleover.Application.DTOs;
using Roleover.Application.Exceptions;
using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;

namespace Roleover.Application.Services;

public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<User> CreateUserAsync(UserCreateDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            throw new ConflictException("A user with the given email already exists.");
        }

        existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
        {
            throw new ConflictException("A user with the given username already exists.");
        }

        var passwordHash = _passwordHasher.HashPassword(dto.Password);

        var user = new User(dto.Username, dto.Email, passwordHash);
        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(string id, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found.");

        if (!string.IsNullOrEmpty(dto.Email))
        {
            user.UpdateEmail(dto.Email);
        }

        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.UpdatePasswordHash(_passwordHasher.HashPassword(dto.Password));
        }

        await _userRepository.UpdateAsync(user);
        return user;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found.");

        await _userRepository.DeleteAsync(user);
        return true;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found.");
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email) ?? throw new NotFoundException("User not found.");
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username) ?? throw new NotFoundException("User not found.");
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}
