﻿using Postter.Controllers.Post.Model;
using Postter.Infrastructure.DAO;

namespace Postter.UseCases.UseCasePost;

public interface IUseCasePost
{
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
    Task AddPostAsync(AddPostModel model);
    /// <summary>
    /// Удалить пост
    /// </summary>
    /// <param name="postId">Id поста</param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId);
}