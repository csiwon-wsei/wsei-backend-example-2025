using ApplicationCore.Application.Commons;

namespace ApplicationCore.Domain.Models;

public class User(string username, string email): BaseIdentity
{
    public string Username { get; set; } = username;
    public string Email { get; set; } = email;
}