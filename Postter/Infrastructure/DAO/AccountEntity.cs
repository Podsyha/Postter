using System.ComponentModel.DataAnnotations;
using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

/// <summary>
/// Сущность пользователя
/// </summary>
public class AccountEntity : EntityBase
{
    public AccountEntity()
    {
        ImageUri = $"testlink.com/testdirectory/testimage.png";
        IsActive = true;
    }
    /// <summary>
    /// Почта
    /// </summary>
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [MaxLength(80)]
    public string Name { get; set; }
    /// <summary>
    /// Путь к фото пользователя
    /// </summary>
    [DataType(DataType.Url)]
    public string ImageUri { get; set; }
    /// <summary>
    /// Инфо о пользователе
    /// </summary>
    [MaxLength(140)]
    public string About { get; set; }
    /// <summary>
    /// Хешированный пароль
    /// </summary>
    public string HashPassword { get; set; }
    /// <summary>
    /// Соль
    /// </summary>
    public string Salt { get; set; }
    /// <summary>
    /// Параметр активности аккаунта(false - удалён)
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Id роли
    /// </summary>
    public int RoleId { get; set; }
    /// <summary>
    /// Роль
    /// </summary>
    public RoleEntity Role { get; set; }
    /// <summary>
    /// Лайки, поставленные пользователем
    /// </summary>
    public ICollection<LikeEntity> Likes { get; set; }
    /// <summary>
    /// Посты, написанные пользователем
    /// </summary>
    public ICollection<PostEntity> Posts { get; set; }
    /// <summary>
    /// Комменты, написанные пользователем
    /// </summary>
    public ICollection<CommentEntity> Comments { get; set; }
}