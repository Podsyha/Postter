using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public class Person : EntityBase
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}