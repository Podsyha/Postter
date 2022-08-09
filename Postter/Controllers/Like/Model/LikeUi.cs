using Postter.Controllers.User.Model;

namespace Postter.Controllers.Like.Model;

public class LikeUi
{
    public Guid Id { get; set; }

    public UserUi User { get; set; }

    public DateTime DateAdd { get; set; }

    public DateTime DateUpdate { get; set; }
}