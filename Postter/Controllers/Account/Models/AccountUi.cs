namespace Postter.Controllers.Account.Models;

public sealed class AccountUi
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Путь к фото пользователя
    /// </summary>
    public string ImageUri { get; set; }
    /// <summary>
    /// Инфо о пользователе
    /// </summary>
    public string About { get; set; }
    /// <summary>
    /// Параметр активности аккаунта(false - удалён)
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Роль
    /// </summary>
    public string RoleName { get; set; }
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime DateAdded { get; set; }
}