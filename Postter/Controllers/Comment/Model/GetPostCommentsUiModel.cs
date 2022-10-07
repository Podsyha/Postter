using System.ComponentModel.DataAnnotations;

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
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых комментариев на странице
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Count { get; set; }
}