using System.ComponentModel;

namespace Postter.Infrastructure.DTO;

/// <summary>
/// Enum всех ролей в системе
/// </summary>
public enum RolesEnum
{
    /// <summary>
    /// Роль администратора
    /// </summary>
    [Description("Администратор")]
    Admin = 1,
    /// <summary>
    /// Роль полдьзователя
    /// </summary>
    [Description("Пользователь")]
    User = 2,
    /// <summary>
    /// Роль модератора
    /// </summary>
    [Description("Модератор")]
    Moder = 3
}