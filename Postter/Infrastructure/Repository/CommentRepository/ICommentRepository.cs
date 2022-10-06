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
    /// Получить все комменты пользователя
    /// </summary>
    /// <param name="authorId">Id автора коммента</param>
    /// <returns></returns>
    Task<List<CommentEntity>> GetAuthorCommentsAsync(Guid authorId);
    /// <summary>
    /// Получить все коммента поста
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task<List<CommentEntity>> GetPostCommentsAsync(Guid postId);
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
    Task AddCommentAsync(CommentEntity newComment);
    /// <summary>
    /// Удалить коммент
    /// </summary>
    /// <param name="commentId">Id коммента</param>
    /// <returns></returns>
    Task DeleteCommentAsync(Guid commentId);
}