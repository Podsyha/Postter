namespace Postter.Controllers.Comment.Model;

/// <summary>
/// Модель комментария для UI
/// </summary>
public sealed class CommentUi
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorImageUri { get; set; }
    public Guid PostId { get; set; }
    public string Text { get; set; }
    public DateTime DateAdded { get; set; }
}