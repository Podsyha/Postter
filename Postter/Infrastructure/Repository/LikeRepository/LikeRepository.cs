using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Controllers.Like.Model;
using Postter.Controllers.Model;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.LikeRepository;

public class LikeRepository : AppDbFunc, ILikeRepository
{
    public LikeRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
        _queryInclude = _dbContext.Like
            .Include(x => x.Author);
    }

    private readonly IAssert _assert;
    private readonly IQueryable<LikeEntity> _queryInclude;

    public async Task<CollectionEntityUi<LikeUi>> GetAuthorLikesUiAsync(Guid authorId, int page, int count)
    {
        int totalCount = _dbContext.Like
            .Count(x => x.AuthorId == authorId);
        IQueryable<LikeUi> query = _queryInclude
            .Where(x => x.AuthorId == authorId)
            .Select(x => new LikeUi()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri
            })
            .Skip((page - 1) * count)
            .Take(count);
        
        double totalPages = Convert.ToDouble(totalCount) / Convert.ToDouble(count);
        CollectionEntityUi<LikeUi> collectionLikes = new()
        {
            Items = await query.ToListAsync(),
            TotalCount = totalCount,
            TotalPages = Math.Ceiling(totalPages)
        };

        return collectionLikes;
    }

    public async Task<CollectionEntityUi<LikeUi>> GetAuthorPostLikesUiAsync(Guid authorId, Guid postId, int page, int count)
    {
        int totalCount = _dbContext.Like
            .Where(x => x.AuthorId == authorId)
            .Count(x => x.PostId == postId);
        IQueryable<LikeUi> query = _queryInclude
            .Where(x => x.AuthorId == authorId)
            .Where(x => x.PostId == postId)
            .Select(x => new LikeUi()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri
            })
            .Skip((page - 1) * count)
            .Take(count);
        
        double totalPages = Convert.ToDouble(totalCount) / Convert.ToDouble(count);
        CollectionEntityUi<LikeUi> collectionLikes = new()
        {
            Items = await query.ToListAsync(),
            TotalCount = totalCount,
            TotalPages = Math.Ceiling(totalPages)
        };

        return collectionLikes;
    }

    public async Task<CollectionEntityUi<LikeUi>> GetPostLikesUiAsync(Guid postId, int page, int count)
    {
        int totalCount = _dbContext.Like
            .Count(x => x.PostId == postId);
        IQueryable<LikeUi> query = _queryInclude
            .Where(x => x.PostId == postId)
            .Select(x => new LikeUi()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                PostId = x.PostId,
                AuthorImageUri = x.Author.ImageUri
            })
            .Skip((page - 1) * count)
            .Take(count);
        
        double totalPages = Convert.ToDouble(totalCount) / Convert.ToDouble(count);
        CollectionEntityUi<LikeUi> collectionLikes = new()
        {
            Items = await query.ToListAsync(),
            TotalCount = totalCount,
            TotalPages = Math.Ceiling(totalPages)
        };

        return collectionLikes;
    }
    
    public async Task<List<LikeEntity>> GetAuthorLikesAsync(Guid authorId)
    {
        IQueryable<LikeEntity> query = _queryInclude
            .Where(x => x.AuthorId == authorId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<List<LikeEntity>> GetAuthorPostLikesAsync(Guid authorId, Guid postId)
    {
        IQueryable<LikeEntity> query = _queryInclude
            .Where(x => x.AuthorId == authorId)
            .Where(x => x.PostId == postId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<List<LikeEntity>> GetPostLikesAsync(Guid postId)
    {
        IQueryable<LikeEntity> query = _queryInclude
            .Where(x => x.PostId == postId);

        List<LikeEntity> likes = await query.ToListAsync();

        return likes;
    }

    public async Task<LikeEntity> FindLikeAsync(Guid likeId)
    {
        IQueryable<LikeEntity> query = _queryInclude
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