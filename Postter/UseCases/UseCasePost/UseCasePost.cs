using Postter.Common.Assert;
using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.PostRepository;

namespace Postter.UseCases.UseCasePost;

public class UseCasePost : IUseCasePost
{
    public UseCasePost(IPostRepository postRepository, IAssert assert)
    {
        _postRepository = postRepository;
        _assert = assert;
    }

    private readonly IPostRepository _postRepository;
    private readonly IAssert _assert;

    public async Task<PostUi> GetPostUiAsync(Guid postId) =>
        await _postRepository.GetPostUiAsync(postId);

    public async Task<CollectionPostUi> GetAuthorPostsUiAsync(Guid authorId, int page, int count) =>
        await _postRepository.GetAuthorPostsUiAsync(authorId, page, count);

    public async Task<PostEntity> GetPostAsync(Guid postId) =>
        await _postRepository.GetPostAsync(postId);

    public async Task<List<PostEntity>> GetAuthorPostsAsync(Guid authorId) =>
        await _postRepository.GetAuthorPostsAsync(authorId);

    public async Task AddPostAsync(AddPostModel model)
    {
        PostEntity post = new()
        {
            AuthorId = model.AuthorId,
            Text = model.Text
        };

        await _postRepository.AddPostAsync(post);
    }
    
    public async Task DeletePostAsync(Guid postId) =>
        await _postRepository.DeletePostAsync(postId);

    public async Task DeletePostAsync(Guid postId, Guid userId)
    {
        PostEntity post = await _postRepository.GetPostAsync(postId);
        _assert.ThrowIfFalse(post.AuthorId == userId, "Вы не можете удалять посты других пользователей");
        
        await _postRepository.DeletePostAsync(postId);
    }
}