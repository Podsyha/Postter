using Postter.Controllers.User.Model;

namespace Postter.Controllers.Comment.Model;

public class CommentUi
{
    public Guid Id { get; set; }
    
    public UserUi User { get; set; }
    
    public string Text { get; set; }

    public DateTime DateAdd { get; set; }

    public DateTime DateUpdate { get; set; }
}