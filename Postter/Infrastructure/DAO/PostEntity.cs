using System.ComponentModel.DataAnnotations;
using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;
/// <summary>
/// Сущность постов
/// </summary>
public class PostEntity : EntityBase
{
    /// <summary>
    /// Id автора поста
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Автор поста
    /// </summary>
    public AccountEntity Author { get; set; }
    /// <summary>
    /// Текст поста
    /// </summary>
    [MaxLength(140)]
    public string Text { get; set; }
    /// <summary>
    /// Лайки поставленные посту
    /// </summary>
    public ICollection<LikeEntity> Likes { get; set; }
    /// <summary>
    /// Комментарии, оставленные под постом
    /// </summary>
    public ICollection<CommentEntity> Comments { get; set; }
}