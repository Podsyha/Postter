using System.ComponentModel.DataAnnotations;

namespace Postter.Controllers.Like.Model;

public sealed class GetAuthorPostLikesUiModel
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid AuthorId { get; set; }
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
    /// Кол-во лайков на странице
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Count { get; set; }
}