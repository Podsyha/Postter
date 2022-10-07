using System.ComponentModel.DataAnnotations;

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
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Page { get; set; }
    /// <summary>
    /// Кол-во загружаемых постов на странице
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Введите значение больше {0}")]
    public int Count { get; set; }
}