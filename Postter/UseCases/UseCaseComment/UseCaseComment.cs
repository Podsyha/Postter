using Postter.Common.Assert;
using Postter.Controllers.Comment.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.CommentRepository;

namespace Postter.UseCases.UseCaseComment;

public class UseCaseComment : IUseCaseComment
{
    public UseCaseComment(ICommentRepository commentRepository, IAssert assert)
    {
        _commentRepository = commentRepository;
        _assert = assert;
    }

    
    private readonly ICommentRepository _commentRepository;
    private readonly IAssert _assert;
    
    public async Task<CommentEntity> GetCommentAsync(Guid commentId)
    {
        return await _commentRepository.FindCommentAsync(commentId);
    }

    public async Task<List<CommentEntity>> GetAuthorCommentsAsync(Guid authorId)
    {
        return await _commentRepository.GetAuthorCommentsAsync(authorId);
    }

    public async Task<List<CommentEntity>> GetPostCommentsAsync(Guid postId)
    {
        return await _commentRepository.GetPostCommentsAsync(postId);
    }

    public async Task<CommentEntity> FindCommentAsync(Guid commentId)
    {
        return await _commentRepository.FindCommentAsync(commentId);
    }

    public async Task AddCommentAsync(AddCommentModel newComment)
    {
        CommentEntity comment = new()
        {
            Text = newComment.Text,
            AuthorId = newComment.AuthorId,
            PostId = newComment.PostId
        };
        
        await _commentRepository.AddCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        await _commentRepository.DeleteCommentAsync(commentId);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid userId)
    {
        CommentEntity comment = await _commentRepository.GetCommentAsync(commentId);
        _assert.ThrowIfFalse(comment.AuthorId == userId, "Вы не можете удалять комментарии других пользователей");
        
        await _commentRepository.DeleteCommentAsync(commentId);
    }
}