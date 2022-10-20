namespace Postter.Controllers.Post.Model;

/// <summary>
/// Модель поста для UI
/// </summary>
public sealed class PostUi
{
    /// <summary>
    /// Id поста
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Id автора поста
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Текст поста
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Имя автора поста
    /// </summary>
    public string AuthorName { get; set; }
    /// <summary>
    /// Ссылка на фото автора поста
    /// </summary>
    public string AuthorImageUri { get; set; }
    /// <summary>
    /// Кол-во лайков на посте
    /// </summary>
    public int CountLikes { get; set; }
    /// <summary>
    /// Кол-во комментариев под постом
    /// </summary>
    public int CountComments { get; set; }
    /// <summary>
    /// Дата добавления поста
    /// </summary>
    public DateTime DateAdded { get; set; }
}