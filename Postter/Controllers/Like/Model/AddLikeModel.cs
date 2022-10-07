namespace Postter.Controllers.Like.Model;

public sealed class AddLikeModel
{
    /// <summary>
    /// Id автора лайка
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Id поста
    /// </summary>
    public Guid PostId { get; set; }
}