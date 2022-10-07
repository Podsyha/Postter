using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.LikeRepository;

public class LikeRepository : AppDbFunc, ILikeRepository
{
    public LikeRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
        _likeQuery = _dbContext.Like
            .Include(x => x.Post)
            .Include(x => x.Author);
    }

    private readonly IAssert _assert;
    private readonly IQueryable<LikeEntity> _likeQuery;

    public async Task<List<LikeEntity>> GetAuthorLikesAsync(Guid authorId)
    {
        IQueryable<LikeEntity> query = _likeQuery
            .Where(x => x.AuthorId == authorId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<List<LikeEntity>> GetAuthorPostLikesAsync(Guid authorId, Guid postId)
    {
        IQueryable<LikeEntity> query = _likeQuery
            .Where(x => x.AuthorId == authorId)
            .Where(x => x.PostId == postId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<List<LikeEntity>> GetPostLikesAsync(Guid postId)
    {
        IQueryable<LikeEntity> query = _likeQuery
            .Where(x => x.PostId == postId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<LikeEntity> FindLikeAsync(Guid likeId)
    {
        IQueryable<LikeEntity> query = _likeQuery
            .Where(x => x.Id == likeId);

        LikeEntity like = await query.FirstOrDefaultAsync();

        _assert.IsNull(like);

        return like;
    }

    public async Task AddLikesAsync(LikeEntity newLike)
    {
        await AddModelAsync(newLike);

        await SaveChangeAsync();
    }

    public async Task DeleteLikesAsync(Guid likeId)
    {
        RemoveModel(await FindLikeAsync(likeId));

        await SaveChangeAsync();
    }

    public async Task<bool> CheckLikedPost(Guid authorId, Guid postId)
    {
        List<LikeEntity> authorLikes = await GetAuthorPostLikesAsync(authorId, postId);

        return authorLikes.Any();
    }
}