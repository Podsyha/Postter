using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

/// <summary>
/// Сущность пользователя
/// </summary>
public class Person : EntityBase
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Хешированный пароль
    /// </summary>
    public string HashPassword { get; set; }
    /// <summary>
    /// Соль
    /// </summary>
    public string Salt { get; set; }
    /// <summary>
    /// Id роли
    /// </summary>
    public int RoleId { get; set; }
    /// <summary>
    /// Роль
    /// </summary>
    public Role Role { get; set; }
}