using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;

namespace Postter.Infrastructure.Repository;

public class UserRepository : AppDbFunc, IUserRepository
{
    public UserRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
    }

    private readonly IAssert _assert;



    /// <summary>
    /// Получить DTO модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<UserDto> GetUserAsync(string password, string email)
    {
        IQueryable<UserDto> query = _dbContext.User
            .AsNoTracking()
            .Where(user => user.Email == email && user.Password == password)
            .Select(user => new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            });

        UserDto user = await query.FirstOrDefaultAsync();

        return user;
    }

    /// <summary>
    /// Найти DTO модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<UserDto> FindUserAsync(string password, string email)
    {
        IQueryable<UserDto> query = _dbContext.User
            .AsNoTracking()
            .Where(user => user.Email == email && user.Password == password)
            .Select(user => new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            });

        UserDto user = await query.FirstOrDefaultAsync();
        
        _assert.IsNull(user);

        return user;
    }

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public async Task AddUserAsync(string email, string password)
    {
        User userModel = new(email, password);

        await AddModel(userModel);
    }
}