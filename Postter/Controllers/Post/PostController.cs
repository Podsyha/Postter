using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
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


    [HttpGet("/get-post-ui")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostUi(Guid postId)
    {
        PostUi post = await _useCasePost.GetPostUiAsync(postId);

        return Ok(post);
    }

    [HttpGet("/get-author-posts-ui")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorPostsUi([FromQuery]GetAuthorPostsUiModel model)
    {
        CollectionPostUi posts = await _useCasePost.GetAuthorPostsUiAsync(model.AuthorId, model.Page, model.Count);

        return Ok(posts);
    }
    
    [HttpGet("/get-post")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPost(Guid postId)
    {
        PostEntity post = await _useCasePost.GetPostAsync(postId);

        return Ok(post);
    }

    [HttpGet("/get-author-posts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorPosts(Guid authorId)
    {
        List<PostEntity> posts = await _useCasePost.GetAuthorPostsAsync(authorId);

        return Ok(posts);
    }

    [HttpPost("/add-post")]
    [CustomAuthorize]
    public async Task<IActionResult> AddPost(AddPostModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCasePost.AddPostAsync(model);

        return Ok();
    }

    [HttpDelete("/delete-post")]
    [CustomAuthorize]
    public async Task DeletePost(Guid postId)
    {
        if (IsCurrentRole(RolesEnum.Admin) || IsCurrentRole(RolesEnum.Moder))
        {
            await _useCasePost.DeletePostAsync(postId);
        }
        else
        {
            Guid authorId = new Guid(HttpContext.User.Identity.GetUserId());
            await _useCasePost.DeletePostAsync(postId, authorId);
        }
    }
}