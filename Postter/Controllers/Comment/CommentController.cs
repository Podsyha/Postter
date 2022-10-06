using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Comment.Model;
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

    [HttpGet("/comment")]
    [AllowAnonymous]
    public async Task<IActionResult> GetComment(Guid commentId)
    {
        CommentEntity post = await _useCaseComment.GetCommentAsync(commentId);

        return Ok(post);
    }

    [HttpGet("/author-сomments")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorComment(Guid authorId)
    {
        List<CommentEntity> posts = await _useCaseComment.GetAuthorCommentsAsync(authorId);

        return Ok(posts);
    }
    
    [HttpGet("/post-сomments")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostComments(Guid postId)
    {
        List<CommentEntity> posts = await _useCaseComment.GetPostCommentsAsync(postId);

        return Ok(posts);
    }

    [HttpPost("/comment")]
    [CustomAuthorize]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        model.AuthorId = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCaseComment.AddCommentAsync(model);

        return Ok();
    }

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