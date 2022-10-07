namespace Postter.Controllers.Post.Model;

public sealed class GetAuthorPostsUiModel
{
    /// <summary>
    /// Id автора поста
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Страница
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых постов на странице
    /// </summary>
    public int Count { get; set; }
}