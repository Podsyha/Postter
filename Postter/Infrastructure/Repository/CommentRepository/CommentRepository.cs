using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Controllers.Comment.Model;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.CommentRepository;

public class CommentRepository : AppDbFunc, ICommentRepository
{
    public CommentRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
        _queryInclude = _dbContext.Comment
            .Include(x => x.Author);
    }

    private readonly IAssert _assert;
    private readonly IQueryable<CommentEntity> _queryInclude;

    public async Task<CommentUi> GetCommentUiAsync(Guid commentId)
    {
        IQueryable<CommentUi> commentQuery = _queryInclude
            .Where(x => x.Id == commentId)
            .Select(x => new CommentUi()
            {
                Id = x.Id,
                Text = x.Text,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                DateAdded = x.DateAdded,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri
            });

        return await commentQuery.FirstOrDefaultAsync();
    }
    
    public async Task<CollectionCommentUi> GetAuthorCommentsUiAsync(Guid authorId, int page, int count)
    {
        IQueryable<CommentUi> query = _queryInclude
            .Where(x => x.AuthorId == authorId)
            .Select(x => new CommentUi()
            {
                Id = x.Id,
                Text = x.Text,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri,
                DateAdded = x.DateAdded
            })
            .Skip((page - 1) * count)
            .Take(count);

        ICollection<CommentUi> comments = await query.ToListAsync();
        CollectionCommentUi collectionPosts = new()
        {
            Comments = comments,
            CommentCount = comments.Count
        };

        return collectionPosts;
    }
    
    public async Task<CollectionCommentUi> GetPostCommentsUiAsync(Guid postId, int page, int count)
    {
        IQueryable<CommentUi> query = _queryInclude
            .Where(x => x.PostId == postId)
            .Select(x => new CommentUi()
            {
                Id = x.Id,
                Text = x.Text,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri,
                DateAdded = x.DateAdded
            })
            .Skip((page - 1) * count)
            .Take(count);

        ICollection<CommentUi> comments = await query.ToListAsync();
        CollectionCommentUi collectionPosts = new()
        {
            Comments = comments,
            CommentCount = comments.Count
        };

        return collectionPosts;
    }
    
    public async Task<CommentEntity> GetCommentAsync(Guid commentId)
    {
        IQueryable<CommentEntity> query = _queryInclude
            .Where(x => x.Id == commentId);

        CommentEntity comment = await query.FirstOrDefaultAsync();

        return comment;
    }

    public async Task<List<CommentEntity>> GetAuthorCommentsAsync(Guid authorId)
    {
        IQueryable<CommentEntity> query = _queryInclude
            .Where(x => x.AuthorId == authorId);

        List<CommentEntity> comment = await query.ToListAsync();

        return comment;
    }

    public async Task<List<CommentEntity>> GetPostCommentsAsync(Guid postId)
    {
        IQueryable<CommentEntity> query = _queryInclude
            .Where(x => x.PostId == postId);

        List<CommentEntity> comment = await query.ToListAsync();

        return comment;
    }

    public async Task<CommentEntity> FindCommentAsync(Guid commentId)
    {
        IQueryable<CommentEntity> query = _queryInclude
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