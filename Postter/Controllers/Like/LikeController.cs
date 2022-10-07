using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Like.Model;
using Postter.Controllers.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
using Postter.UseCases.UseCaseLike;

namespace Postter.Controllers.Like;

/// <summary>
/// Контроллер комментариев
/// </summary>
[ApiController]
[Route("[controller]")]
public class LikeController : CustomController
{
    public LikeController(IUseCaseLike useCaseLike)
    {
        _useCaseLike = useCaseLike;
    }

    private readonly IUseCaseLike _useCaseLike;

    [HttpGet("/author-likes-ui")]
    [AllowAnonymous]
    public async Task<CollectionEntityUi<LikeUi>> GetAuthorLikesUiAsync([FromQuery] GetAuthorLikesUiModel model) =>
        await _useCaseLike.GetAuthorLikesUiAsync(model.AuthorId, model.Page, model.Count);

    [HttpGet("/author-post-likes-ui")]
    [AllowAnonymous]
    public async Task<CollectionEntityUi<LikeUi>> GetAuthorPostLikesUiAsync([FromQuery] GetAuthorPostLikesUiModel model) =>
        await _useCaseLike.GetAuthorPostLikesUiAsync(model.AuthorId, model.PostId, model.Page, model.Count);

    [HttpGet("/post-likes-ui")]
    [AllowAnonymous]
    public async Task<CollectionEntityUi<LikeUi>> GetPostLikesUiAsync([FromQuery]GetPostLikesUiModel model) =>
        await _useCaseLike.GetPostLikesUiAsync(model.PostId, model.Page, model.Count);

    [HttpGet("/author-likes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorLikes(Guid commentId)
    {
        List<LikeEntity> posts = await _useCaseLike.GetAuthorLikesAsync(commentId);

        return Ok(posts);
    }

    [HttpGet("/post-likes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostLikes(Guid authorId)
    {
        List<LikeEntity> posts = await _useCaseLike.GetPostLikesAsync(authorId);

        return Ok(posts);
    }

    [HttpPost("/like")]
    [CustomAuthorize]
    public async Task<IActionResult> AddLike(AddLikeModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCaseLike.AddLikeAsync(model);

        return Ok();
    }

    [HttpDelete("/like")]
    [CustomAuthorize]
    public async Task DeleteLikes(Guid likeId)
    {
        if (IsCurrentRole(RolesEnum.Admin) || IsCurrentRole(RolesEnum.Moder))
        {
            await _useCaseLike.DeleteLikesAsync(likeId);
        }
        else
        {
            Guid authorId = new Guid(HttpContext.User.Identity.GetUserId());
            await _useCaseLike.DeleteLikesAsync(likeId, authorId);
        }
    }
}