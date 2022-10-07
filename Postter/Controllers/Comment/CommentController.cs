using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Comment.Model;
using Postter.Controllers.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
using Postter.UseCases.UseCaseComment;

namespace Postter.Controllers.Comment;

/// <summary>
/// Контроллер комментариев
/// </summary>
[ApiController]
[Route("[controller]")]
public class CommentController : CustomController
{
    public CommentController(IUseCaseComment useCaseComment)
    {
        _useCaseComment = useCaseComment;
    }

    private readonly IUseCaseComment _useCaseComment;
    
    /// <summary>
    /// Получить комментарий
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    [HttpGet("/comment")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCommentUi(Guid commentId)
    {
        CommentUi post = await _useCaseComment.GetCommentUiAsync(commentId);

        return Ok(post);
    }
    
    /// <summary>
    /// Получить комментарии пользователя 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/author-сomments")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorCommentUi([FromQuery]GetAuthorCommentsUiModel model)
    {
        CollectionEntityUi<CommentUi> posts = await _useCaseComment.GetAuthorCommentsUiAsync(model.AuthorId, model.Page, model.Count);

        return Ok(posts);
    }
    
    /// <summary>
    /// Получить комментарии поста
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/post-сomments")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostCommentsUi([FromQuery]GetPostCommentsUiModel model)
    {
        CollectionEntityUi<CommentUi> posts = await _useCaseComment.GetPostCommentsUiAsync(model.PostId, model.Page, model.Count);

        return Ok(posts);
    }
    
    /// <summary>
    /// Добавить комментарий
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("/comment")]
    [CustomAuthorize]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCaseComment.AddCommentAsync(model);

        return Ok();
    }

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    /// <param name="commentId"></param>
    [HttpDelete("/comment")]
    [CustomAuthorize]
    public async Task DeleteComment(Guid commentId)
    {
        if (IsCurrentRole(RolesEnum.Admin) || IsCurrentRole(RolesEnum.Moder))
        {
            await _useCaseComment.DeleteCommentAsync(commentId);
        }
        else
        {
            Guid authorId = new Guid(HttpContext.User.Identity.GetUserId());
            await _useCaseComment.DeleteCommentAsync(commentId, authorId);
        }
    }
}