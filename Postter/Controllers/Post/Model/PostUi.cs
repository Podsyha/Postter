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
}