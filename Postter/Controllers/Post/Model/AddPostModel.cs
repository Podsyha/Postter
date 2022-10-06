using System.ComponentModel.DataAnnotations;

namespace Postter.Controllers.Post.Model;

public class AddPostModel
{
    /// <summary>
    /// Id автора поста
    /// </summary>
    internal Guid AuthorId { get; set; }
    /// <summary>
    /// Текст поста
    /// </summary>
    [MaxLength(140)]
    public string Text { get; set; }
}