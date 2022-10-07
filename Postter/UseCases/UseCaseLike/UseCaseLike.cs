using Postter.Common.Assert;
using Postter.Controllers.Like.Model;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.LikeRepository;

namespace Postter.UseCases.UseCaseLike;

public class UseCaseLike : IUseCaseLike
{
    public UseCaseLike(ILikeRepository likeRepository, IAssert assert)
    {
        _likeRepository = likeRepository;
        _assert = assert;
    }

    private readonly ILikeRepository _likeRepository;
    private readonly IAssert _assert;

    public async Task<List<LikeEntity>> GetAuthorLikesAsync(Guid authorId) =>
        await _likeRepository.GetAuthorLikesAsync(authorId);

    public async Task<List<LikeEntity>> GetPostLikesAsync(Guid postId) =>
        await _likeRepository.GetPostLikesAsync(postId);

    public async Task AddLikeAsync(AddLikeModel addLike)
    {
        LikeEntity like = new()
        {
            AuthorId = addLike.AuthorId,
            PostId = addLike.PostId
        };

        await _likeRepository.AddLikesAsync(like);
    }

    public async Task DeleteLikesAsync(Guid likeId) =>
        await _likeRepository.DeleteLikesAsync(likeId);

    public async Task DeleteLikesAsync(Guid likeId, Guid authorId)
    {
        LikeEntity comment = await _likeRepository.FindLikeAsync(likeId);
        _assert.ThrowIfFalse(comment.AuthorId == authorId, "Вы не можете удалять лайки других пользователей");
        
        await _likeRepository.DeleteLikesAsync(likeId);
    }
}