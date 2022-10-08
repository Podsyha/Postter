using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Common.Helpers;
using Postter.Controllers.Account.Models;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.AccountRepository;

public class AccountRepository : AppDbFunc, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext, IAssert assert, IRegistrationHelper registrationHelper)
        : base(dbContext, assert)
    {
        _assert = assert;
        _registrationHelper = registrationHelper;
    }

    
    private readonly IAssert _assert;
    private readonly IRegistrationHelper _registrationHelper;


    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    public async Task<AccountUi> GetPersonUiAsync(Guid id)
    {
        IQueryable<AccountUi> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Id == id)
            .Select(x => new AccountUi()
            {
                Id = x.Id,
                About = x.About,
                Email = x.Email,
                Name = x.Name,
                ImageUri = x.ImageUri,
                IsActive = x.IsActive,
                RoleName = x.Role.Name,
                DateAdded = x.DateAdded
            });

        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<AccountEntity> GetPersonAsync(string email, string password)
    {
        AccountEntity currentAccountEntity = await _dbContext.Person
            .FirstOrDefaultAsync(x => x.Email == email);

        _assert.IsNull(currentAccountEntity, $"Не найден пользователь с email: {email}");

        string hashPass = _registrationHelper.generateHashPass(password, currentAccountEntity.Salt);

        AccountEntity accountEntity = await _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.HashPassword == hashPass)
            .FirstOrDefaultAsync();

        return accountEntity;
    }

    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<AccountEntity> GetPersonAsync(string email)
    {
        IQueryable<AccountEntity> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email);

        AccountEntity accountEntity = await query.FirstOrDefaultAsync();

        return accountEntity;
    }

    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    public async Task<AccountEntity> GetPersonAsync(Guid id)
    {
        IQueryable<AccountEntity> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Id == id);

        AccountEntity accountEntity = await query.FirstOrDefaultAsync();

        return accountEntity;
    }

    /// <summary>
    /// Обновить сущность пользователя
    /// </summary>
    /// <param name="accountEntity"></param>
    public async Task UpdatePersonInfo(AccountEntity accountEntity)
    {
        AccountEntity currentAccountEntity = await FindPersonAsync(accountEntity.Email);
        currentAccountEntity = accountEntity;

        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<AccountEntity> FindPersonAsync(string email, string password)
    {
        AccountEntity currentAccountEntity = await _dbContext.Person
            .FirstOrDefaultAsync(x => x.Email == email);

        _assert.IsNull(currentAccountEntity, $"Не найден пользователь с email: {email}");

        string hashPass = _registrationHelper.generateHashPass(password, currentAccountEntity.Salt);

        AccountEntity accountEntity = await _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.HashPassword == hashPass)
            .FirstOrDefaultAsync();

        _assert.IsNull(accountEntity);

        return accountEntity;
    }

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<AccountEntity> FindPersonAsync(string email)
    {
        IQueryable<AccountEntity> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email);

        AccountEntity accountEntity = await query.FirstOrDefaultAsync();
        _assert.IsNull(accountEntity);

        return accountEntity;
    }

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public async Task<AccountEntity> FindPersonAsync(Guid accountId)
    {
        IQueryable<AccountEntity> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Id == accountId);

        AccountEntity accountEntity = await query.FirstOrDefaultAsync();
        _assert.IsNull(accountEntity);

        return accountEntity;
    }

    /// <summary>
    /// Проверить уникальность почты
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns>True - пользователя с постой нет в системе</returns>
    public async Task<bool> CheckMailUniqueness(string email)
    {
        AccountEntity accountEntity = await GetPersonAsync(email);

        return accountEntity == null;
    }

    /// <summary>
    /// Добавить сущность пользователя
    /// </summary>
    /// <param name="newAccountEntity">Пользователь</param>
    public async Task<AccountUi> AddPerson(AccountEntity newAccountEntity)
    {
        await AddModelAsync(newAccountEntity);
        await SaveChangeAsync();

        return await GetPersonUiAsync(newAccountEntity.Id);
    }

    /// <summary>
    /// Отметить аккаунт удалённым
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public async Task DeleteAccount(Guid accountId)
    {
        AccountEntity account = await FindPersonAsync(accountId);
        account.IsActive = false;

        await SaveChangeAsync();
    }
}