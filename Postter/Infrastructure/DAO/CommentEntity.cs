using System.ComponentModel.DataAnnotations;
using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;
/// <summary>
/// Сущность комментариев
/// </summary>
public class CommentEntity : EntityBase
{
    /// <summary>
    /// Id автора комментария
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Автор комментария
    /// </summary>
    public AccountEntity Author { get; set; }
    /// <summary>
    /// ID поста, где оставлен комментарий
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Пост, где оставлен комментарий
    /// </summary>
    public PostEntity Post { get; set; }
    /// <summary>
    /// Текст комментария
    /// </summary>
    [MaxLength(140)]
    public string Text { get; set; }
}