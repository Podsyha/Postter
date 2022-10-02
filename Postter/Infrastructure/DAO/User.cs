using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public sealed class User : EntityBase
{
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
    
    public string Email { get; set; }
    public string Password { get; set; }
}