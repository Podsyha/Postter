using Postter.Controllers.Like.Model;
using Postter.Infrastructure.DAO;

namespace Postter.UseCases.UseCaseLike;

public interface IUseCaseLike
{
    /// <summary>
    /// Получить все лайки пользователя
    /// </summary>
    /// <param name="authorId">Id автора лайков</param>
    /// <returns></returns>
    Task<List<LikeEntity>> GetAuthorLikesAsync(Guid authorId);
    /// <summary>
    /// Получить все лайки поста
    /// </summary>
    /// <param name="postId">Id поста лайков</param>
    /// <returns></returns>
    Task<List<LikeEntity>> GetPostLikesAsync(Guid postId);
    /// <summary>
    /// Добавить лайк
    /// </summary>
    /// <param name="addLike">Коммент</param>
    Task AddLikeAsync(AddLikeModel addLike);
    /// <summary>
    /// Удалить лайк
    /// </summary>
    /// <param name="likeId">Id коммента</param>
    /// <returns></returns>
    Task DeleteLikesAsync(Guid likeId);
    /// <summary>
    /// Удалить лайк
    /// </summary>
    /// <param name="likeId">Id коммента</param>
    /// <param name="authorId">Id автора лайка</param>
    /// <returns></returns>
    Task DeleteLikesAsync(Guid likeId, Guid authorId);
    
}