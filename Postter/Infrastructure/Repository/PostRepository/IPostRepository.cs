using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.PostRepository;

public interface IPostRepository
{
    /// <summary>
    /// Получить модель поста. Без проверки на null
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task<PostEntity> GetPostAsync(Guid postId);
    /// <summary>
    /// Получить модели всех постов пользователя
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
    Task<List<PostEntity>> GetAuthorPostsAsync(Guid authorId);
    /// <summary>
    /// Найти модель поста. Проверка на null
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<PostEntity> FindPostAsync(Guid postId);
    /// <summary>
    /// Добавить сущность поста
    /// </summary>
    /// <param name="newPostEntity">Пост</param>
    Task AddPostAsync(PostEntity newPostEntity);
    /// <summary>
    /// Удалить пост
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId);
}