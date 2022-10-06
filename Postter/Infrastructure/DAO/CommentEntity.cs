using Postter.Infrastructure.Common;

namespace Postter.Infrastructure.DAO;

public class CommentEntity : EntityBase
{
    public Guid AuthorId { get; set; }
    public AccountEntity Author { get; set; }
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }
    public string Text { get; set; }
}