using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Controllers.Model;
using Postter.Controllers.Post.Model;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.PostRepository;

public class PostRepository : AppDbFunc, IPostRepository
{
    public PostRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
        _queryInclude = _dbContext.Post
            .Include(x => x.Author)
            .Include(x => x.Comments)
            .Include(x => x.Likes);
    }

    private readonly IAssert _assert;
    private readonly IQueryable<PostEntity> _queryInclude;


    public async Task<PostUi> GetPostUiAsync(Guid postId)
    {
        IQueryable<PostUi> post = _queryInclude
            .Where(x => x.Id == postId)
            .Select(x => new PostUi()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                AuthorImageUri = x.Author.ImageUri,
                CountComments = x.Comments.Count,
                CountLikes = x.Likes.Count,
                DateAdded = x.DateAdded
            });

        return await post.FirstOrDefaultAsync();
    }

    public async Task<CollectionEntityUi<PostUi>> GetAuthorPostsUiAsync(Guid authorId, int page, int count)
    {
        int totalCount = _dbContext.Post.Count(x => x.AuthorId == authorId);
        IQueryable<PostUi> query = _queryInclude
            .Where(x => x.AuthorId == authorId)
            .Select(x => new PostUi()
            {
                Id = x.Id,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name,
                AuthorImageUri = x.Author.ImageUri,
                CountComments = x.Comments.Count,
                CountLikes = x.Likes.Count,
                DateAdded = x.DateAdded
            })
            .Skip((page - 1) * count)
            .Take(count);
        
        double totalPages = Convert.ToDouble(totalCount) / Convert.ToDouble(count);
        CollectionEntityUi<PostUi> collectionPosts = new()
        {
            Items = await query.ToListAsync(),
            TotalCount = totalCount,
            TotalPages = Math.Ceiling(totalPages)
        };

        return collectionPosts;
    }

    public async Task<PostEntity> GetPostAsync(Guid postId)
    {
        PostEntity post = await _dbContext.Post
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == postId);

        return post;
    }

    public async Task<List<PostEntity>> GetAuthorPostsAsync(Guid authorId)
    {
        IQueryable<PostEntity> query = _dbContext.Post
            .Include(x => x.Author)
            .Where(x => x.AuthorId == authorId);

        List<PostEntity> posts = await query.ToListAsync();

        return posts;
    }

    public async Task<PostEntity> FindPostAsync(Guid postId)
    {
        PostEntity post = await _dbContext.Post
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == postId);

        _assert.IsNull(post, $"Пост не найден.");

        return post;
    }

    public async Task AddPostAsync(PostEntity newPostEntity)
    {
        await AddModelAsync(newPostEntity);

        await SaveChangeAsync();
    }


    public async Task DeletePostAsync(Guid postId)
    {
        PostEntity post = await _dbContext.Post
            .FirstOrDefaultAsync(x => x.Id == postId);

        RemoveModel(post);

        await SaveChangeAsync();
    }
}