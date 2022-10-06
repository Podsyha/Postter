using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.CommentRepository;

public class CommentRepository : AppDbFunc, ICommentRepository
{
    public CommentRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
        _commentQuery = _dbContext.Comment
            .Include(x => x.Post)
            .Include(x => x.Author);
    }

    private readonly IAssert _assert;
    private readonly IQueryable<CommentEntity> _commentQuery;

    public async Task<CommentEntity> GetCommentAsync(Guid commentId)
    {
        IQueryable<CommentEntity> query = _commentQuery
            .Where(x => x.Id == commentId);

        CommentEntity comment = await query.FirstOrDefaultAsync();

        return comment;
    }

    public async Task<List<CommentEntity>> GetAuthorCommentsAsync(Guid authorId)
    {
        IQueryable<CommentEntity> query = _commentQuery
            .Where(x => x.AuthorId == authorId);

        List<CommentEntity> comment = await query.ToListAsync();

        return comment;
    }

    public async Task<List<CommentEntity>> GetPostCommentsAsync(Guid postId)
    {
        IQueryable<CommentEntity> query = _commentQuery
            .Where(x => x.PostId == postId);

        List<CommentEntity> comment = await query.ToListAsync();

        return comment;
    }

    public async Task<CommentEntity> FindCommentAsync(Guid commentId)
    {
        IQueryable<CommentEntity> query = _commentQuery
            .Where(x => x.Id == commentId);

        CommentEntity comment = await query.FirstOrDefaultAsync();

        _assert.IsNull(comment);

        return comment;
    }

    public async Task AddCommentAsync(CommentEntity newComment)
    {
        await AddModelAsync(newComment);

        await SaveChangeAsync();
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        RemoveModel(await FindCommentAsync(commentId));
        
        await SaveChangeAsync();
    }
}