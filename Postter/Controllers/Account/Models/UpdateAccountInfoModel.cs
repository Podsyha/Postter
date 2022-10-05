namespace Postter.Controllers.Account.Models;

/// <summary>
/// Модель для обновления базовой информации пользователя
/// </summary>
public sealed class UpdateAccountInfoModel
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// О себе
    /// </summary>
    public string About { get; set; }
}