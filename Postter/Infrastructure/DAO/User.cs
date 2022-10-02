using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public sealed class User : EntityBase
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}