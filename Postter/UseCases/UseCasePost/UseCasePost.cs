using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.PostRepository;

namespace Postter.UseCases.UseCasePost;

public class UseCasePost : IUseCasePost
{
    public UseCasePost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    private readonly IPostRepository _postRepository;

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
}