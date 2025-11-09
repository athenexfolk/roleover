namespace Roleover.Application.Interfaces;

public interface IAuthProvider
{
    Task<string> AuthenticateAsync(string username, string password);
}
