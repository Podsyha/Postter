using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public class PostEntity : EntityBase
{
    public Guid AuthorId { get; set; }
    public AccountEntity Author { get; set; }
    public string Text { get; set; }
    public ICollection<LikeEntity> Likes { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
}