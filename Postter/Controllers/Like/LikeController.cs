using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Like.Model;
using Postter.Controllers.Model;
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

    /// <summary>
    /// Получить лайки пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/author-likes")]
    [AllowAnonymous]
    public async Task<CollectionEntityUi<LikeUi>> GetAuthorLikesUiAsync([FromQuery] GetAuthorLikesUiModel model) =>
        await _useCaseLike.GetAuthorLikesUiAsync(model.AuthorId, model.Page, model.Count);
    
    /// <summary>
    /// Получить лайки на посте
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/post-likes")]
    [AllowAnonymous]
    public async Task<CollectionEntityUi<LikeUi>> GetPostLikesUiAsync([FromQuery]GetPostLikesUiModel model) =>
        await _useCaseLike.GetPostLikesUiAsync(model.PostId, model.Page, model.Count);

    /// <summary>
    /// Добавить лайк
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("/like")]
    [CustomAuthorize]
    public async Task<IActionResult> AddLike(AddLikeModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCaseLike.AddLikeAsync(model);

        return Ok();
    }

    /// <summary>
    /// Удалить лайк
    /// </summary>
    /// <param name="likeId"></param>
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