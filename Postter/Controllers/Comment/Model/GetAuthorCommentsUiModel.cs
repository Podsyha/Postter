using System.ComponentModel.DataAnnotations;

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
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых комментариев на странице
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Count { get; set; }
}