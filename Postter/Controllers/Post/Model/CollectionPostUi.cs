namespace Postter.Controllers.Post.Model;

/// <summary>
/// Модель коллекции постов для UI
/// </summary>
public class CollectionPostUi
{
    /// <summary>
    /// Коллекция постов
    /// </summary>
    public ICollection<PostUi> Posts { get; set; }
    /// <summary>
    /// Общее кол-во постов
    /// </summary>
    public int CountPosts { get; set; }
}