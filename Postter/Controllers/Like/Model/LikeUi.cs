namespace Postter.Controllers.Like.Model;

/// <summary>
/// Модель лайка для UI
/// </summary>
public sealed class LikeUi
{
    /// <summary>
    /// Id лайка
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Id автора лайка
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Id поста, куда проставлен лайк
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Имя автора лайка
    /// </summary>
    public string AuthorName { get; set; }
    /// <summary>
    /// Ссылка на фото автора лайка
    /// </summary>
    public string AuthorImageUri { get; set; }
}