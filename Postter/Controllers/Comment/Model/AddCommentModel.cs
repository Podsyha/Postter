using System.ComponentModel.DataAnnotations;

namespace Postter.Controllers.Comment.Model;

public sealed class AddCommentModel
{
    /// <summary>
    /// Id автора коммента
    /// </summary>
    internal Guid AuthorId { get; set; }
    /// <summary>
    /// ID поста
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Текст коммента
    /// </summary>
    [MaxLength(140)]
    public string Text { get; set; }
}