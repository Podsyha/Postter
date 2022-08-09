using Postter.Controllers.Comment.Model;
using Postter.Controllers.Post.Model;

namespace Postter.Controllers.User.Model;

public class UserUi
{
    public Guid Id { get; set; }

    public string Login { get; set; }
    
    public List<PostUi> Posts { get; set; }
    
    public List<CommentUi> Comments { get; set; }

    public DateTime DateAdd { get; set; }

    public DateTime DateUpdate { get; set; }
}