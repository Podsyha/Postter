using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.LikeRepository;

public interface ILikeRepository
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
    /// Получить лайки пользователя у поста
    /// </summary>
    /// <param name="authorId">Id пользователя</param>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task<List<LikeEntity>> GetAuthorPostLikesAsync(Guid authorId, Guid postId);
    /// <summary>
    /// Найти лайк. Проверка на null
    /// </summary>
    /// <param name="likeId">Id лайка</param>
    /// <returns></returns>
    Task<LikeEntity> FindLikeAsync(Guid likeId);
    /// <summary>
    /// Добавить лайк
    /// </summary>
    /// <param name="newLike">Коммент</param>
    Task AddLikesAsync(LikeEntity newLike);
    /// <summary>
    /// Удалить лайк
    /// </summary>
    /// <param name="likeId">Id коммента</param>
    /// <returns></returns>
    Task DeleteLikesAsync(Guid likeId);
    /// <summary>
    /// True - пост уже лайкнут пользователем
    /// </summary>
    /// <param name="authorId">Id пользователя</param>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task<bool> CheckLikedPost(Guid authorId, Guid postId);
}