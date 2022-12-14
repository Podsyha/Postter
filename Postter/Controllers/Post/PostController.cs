using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Model;
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
    
    /// <summary>
    /// Получить пост
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/get-post")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostUi([FromQuery] Guid id)
    {
        PostUi post = await _useCasePost.GetPostUiAsync(id);

        return Ok(post);
    }

    /// <summary>
    /// Получить посты автора
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/get-author-posts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorPostsUi([FromQuery]GetAuthorPostsUiModel model)
    {
        CollectionEntityUi<PostUi> posts = await _useCasePost.GetAuthorPostsUiAsync(model.AuthorId, model.Page, model.Count);

        return Ok(posts);
    }

    /// <summary>
    /// Добавить пост
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("/add-post")]
    [CustomAuthorize()]
    public async Task<IActionResult> AddPost(AddPostModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        PostUi post = await _useCasePost.AddPostAsync(model);

        return CreatedAtAction(nameof(GetPostUi), new { id = post.Id }, post);
    }

    /// <summary>
    /// Удалить пост
    /// </summary>
    /// <param name="postId"></param>
    [HttpDelete("/delete-post")]
    [CustomAuthorize]
    public async Task<IActionResult> DeletePost(Guid postId)
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
        
        return NoContent();
    }
}