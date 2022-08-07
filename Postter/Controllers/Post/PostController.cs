using Microsoft.AspNetCore.Mvc;
using Postter.Controllers.Post.Model;

namespace Postter.Controllers.Post;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }
    
    private readonly ILogger<PostController> _logger;

    [HttpGet]
    public PostUi GetPost(Guid id)
    {
        return new PostUi()
        {
            Id = id,
            Likes = 1,
            Reposts = 1,
            DateAdd = DateTime.Now,
            DateUpdate = DateTime.Now
        };
    }
}