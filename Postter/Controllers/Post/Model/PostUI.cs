using Postter.Controllers.Comment.Model;
using Postter.Controllers.Like.Model;
using Postter.Controllers.User.Model;

namespace Postter.Controllers.Post.Model;

public class PostUi
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }

    public UserUi User { get; set; }
    
    public List<LikeUi> Likes { get; set; }

    public List<CommentUi> Comments { get; set; }
    
    public DateTime DateAdd { get; set; }

    public DateTime DateUpdate { get; set; }
}