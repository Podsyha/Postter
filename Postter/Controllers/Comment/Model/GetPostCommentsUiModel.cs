namespace Postter.Controllers.Comment.Model;

/// <summary>
/// Модель для получения UI комментариев поста
/// </summary>
public class GetPostCommentsUiModel
{
    /// <summary>
    /// Id поста
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Страница
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых комментариев на странице
    /// </summary>
    public int Count { get; set; }
}