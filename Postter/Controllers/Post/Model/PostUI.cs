namespace Postter.Controllers.Post.Model;

public class PostUi
{
    public Guid Id { get; set; }

    public int Reposts { get; set; }

    public int Likes { get; set; }
    
    public DateTime DateAdd { get; set; }

    public DateTime DateUpdate { get; set; }
}