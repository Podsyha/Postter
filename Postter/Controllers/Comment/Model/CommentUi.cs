namespace Postter.Controllers.Comment.Model;

/// <summary>
/// Модель комментария для UI
/// </summary>
public sealed class CommentUi
{
    /// <summary>
    /// Id комментария
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Id автоар комментария
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Имя автора комментария
    /// </summary>
    public string AuthorName { get; set; }
    /// <summary>
    /// Ссылка на фото автора комментария
    /// </summary>
    public string AuthorImageUri { get; set; }
    /// <summary>
    /// Id поста комментария
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Дата добавления комментария
    /// </summary>
    public DateTime DateAdded { get; set; }
}