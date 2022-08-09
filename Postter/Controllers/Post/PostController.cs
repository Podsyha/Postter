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
    
}