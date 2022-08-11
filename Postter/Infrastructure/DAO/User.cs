using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public sealed class User : EntityBase
{
    public string Login { get; set; }
}