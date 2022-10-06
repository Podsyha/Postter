using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;
using Postter.UseCases.UseCasePost;

namespace Postter.Controllers.Post;

/// <summary>
/// Контроллер постов
/// </summary>
[ApiController]
[Route("[controller]")]
public class PostController : CustomController
{
    public PostController(IUseCasePost useCasePost)
    {
        _useCasePost = useCasePost;
    }

    private readonly IUseCasePost _useCasePost;

    [HttpGet("/getPost")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPost(Guid postId)
    {
        PostEntity post = await _useCasePost.GetPostAsync(postId);

        return Ok(post);
    }

    [HttpGet("/getAuthorPosts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorPosts(Guid authorId)
    {
         List<PostEntity> posts = await _useCasePost.GetAuthorPostsAsync(authorId);

         return Ok(posts);
    }

    [HttpPost("/addPost")]
    [CustomAuthorize]
    public async Task<IActionResult> AddPost(AddPostModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCasePost.AddPostAsync(model);

        return Ok();
    }
    
    [HttpDelete]
    public async Task DeletePost(Guid postId) =>
        await _useCasePost.DeletePostAsync(postId);
}