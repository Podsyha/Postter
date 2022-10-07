namespace Postter.Controllers.Comment.Model;

/// <summary>
/// Модель коллекции комментариев для UI
/// </summary>
public sealed class CollectionCommentUi
{
    /// <summary>
    /// Комментарии
    /// </summary>
    public ICollection<CommentUi> Comments { get; set; }
    /// <summary>
    /// Общее кол-во комментариев
    /// </summary>
    public int CommentCount { get; set; }
}