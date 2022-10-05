namespace Postter.Controllers.Account.Models;

/// <summary>
/// Модель для регистрации нового пользователя
/// </summary>
public class RegistrationModel
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// О себе
    /// </summary>
    public string About { get; set; }
}