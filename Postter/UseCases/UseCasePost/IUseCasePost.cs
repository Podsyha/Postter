using Postter.Controllers.Model;
using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;

namespace Postter.UseCases.UseCasePost;

public interface IUseCasePost
{
    /// <summary>
    /// Получить UI модель поста
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<PostUi> GetPostUiAsync(Guid postId);
    /// <summary>
    /// Получить все UI модели поста пользователя
    /// </summary>
    /// <param name="authorId"></param>
    /// <returns></returns>
    Task<CollectionEntityUi<PostUi>> GetAuthorPostsUiAsync(Guid authorId, int page, int count);
    /// <summary>
    /// Получить модель поста
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
    /// Добавить сущность поста
    /// </summary>
    /// <param name="model">Модельс оздания поста</param>
    Task<PostUi> AddPostAsync(AddPostModel model);
    /// <summary>
    /// Удалить пост по ID поста
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId);
    /// <summary>
    /// Удалить пост по ID поста с проверкой на доступ к удалению
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <param name="userId">Id пользователя поста</param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId, Guid userId);
}