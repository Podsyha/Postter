using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;
/// <summary>
/// Сущность лайков
/// </summary>
public class LikeEntity : EntityBase
{
    /// <summary>
    /// Id автор лайка
    /// </summary>
    public Guid AuthorId { get; set; }
    /// <summary>
    /// Автор лайка
    /// </summary>
    public AccountEntity Author { get; set; }
    /// <summary>
    /// Id поста, куда поставлен лайк
    /// </summary>
    public Guid PostId { get; set; }
    /// <summary>
    /// Пост, куда поставлен лайк
    /// </summary>
    public PostEntity Post { get; set; }
}