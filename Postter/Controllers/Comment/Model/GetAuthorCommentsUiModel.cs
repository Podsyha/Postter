namespace Postter.Controllers.Comment.Model;

/// <summary>
/// Модель для получения UI комментариев автора
/// </summary>
public class GetAuthorCommentsUiModel
{
    /// <summary>
    /// Id автора комментариев
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Страница
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых комментариев на странице
    /// </summary>
    public int Count { get; set; }
}