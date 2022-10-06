using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.PostRepository;

public class PostRepository : AppDbFunc, IPostRepository
{
    public PostRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
    }
    
    private readonly IAssert _assert;
    
    
    public async Task<PostEntity> GetPostAsync(Guid postId)
    {
        PostEntity post = await _dbContext.Post
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