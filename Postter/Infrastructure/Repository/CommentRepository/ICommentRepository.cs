using Postter.Controllers.Comment.Model;
using Postter.Controllers.Model;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.CommentRepository;

public interface ICommentRepository
{
    /// <summary>
    /// Получить коммент. Без проверки на null
    /// </summary>
    /// <param name="commentId">Id коммента</param>
    /// <returns></returns>
    Task<CommentEntity> GetCommentAsync(Guid commentId);
    /// <summary>
    /// Получить UI модель комментария
    /// </summary>
    /// <param name="commentId">Id комментария</param>
    /// <returns></returns>
    Task<CommentUi> GetCommentUiAsync(Guid commentId);
    /// <summary>
    /// Получить все UI комменты пользователя
    /// </summary>
    /// <param name="authorId">Id автора коммента</param>
    /// <param name="page">Страница</param>
    /// <param name="count">Кол-во комментов на странице</param>
    /// <returns></returns>
    Task<CollectionEntityUi<CommentUi>> GetAuthorCommentsUiAsync(Guid authorId, int page, int count);
    /// <summary>
    /// Получить все UI комменты поста
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <param name="page">Страница</param>
    /// <param name="count">Кол-во комментов на странице</param>
    Task<CollectionEntityUi<CommentUi>> GetPostCommentsUiAsync(Guid postId, int page, int count);
    /// <summary>
    /// Получить все коммента поста
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task<List<CommentEntity>> GetPostCommentsAsync(Guid postId);
    /// <summary>
    /// Получить все комменты пользователя
    /// </summary>
    /// <param name="authorId">Id пользователя</param>
    /// <returns></returns>
    Task<List<CommentEntity>> GetAuthorCommentsAsync(Guid authorId);
    /// <summary>
    /// Найти коммент. Проверка на null
    /// </summary>
    /// <param name="commentId">Id коммента</param>
    /// <returns></returns>
    Task<CommentEntity> FindCommentAsync(Guid commentId);

    /// <summary>
    /// Добавить коммент
    /// </summary>
    /// <param name="newComment">Коммент</param>
    Task<CommentUi> AddCommentAsync(CommentEntity newComment);
    /// <summary>
    /// Удалить коммент
    /// </summary>
    /// <param name="commentId">Id коммента</param>
    /// <returns></returns>
    Task DeleteCommentAsync(Guid commentId);
}