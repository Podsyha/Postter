using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Common.Helpers;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.Persons;

public class PersonRepository : AppDbFunc, IPersonRepository
{
    public PersonRepository(AppDbContext dbContext, IAssert assert, IRegistrationHelper registrationHelper) : base(dbContext, assert)
    {
        _assert = assert;
        _registrationHelper = registrationHelper;
    }

    private readonly IAssert _assert;
    private readonly IRegistrationHelper _registrationHelper;


    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<Person> GetPersonAsync(string email, string password)
    {
        Person currentPerson = await _dbContext.Person
            .FirstOrDefaultAsync(x => x.Email == email);
        
        _assert.IsNull(currentPerson, $"Не найден пользователь с email: {email}");

        string hashPass = _registrationHelper.generateHashPass(password, currentPerson.Salt);
        
        Person person = await _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.HashPassword == hashPass)
            .FirstOrDefaultAsync();

        return person;
    }
    
    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<Person> GetPersonAsync(string email)
    {
        IQueryable<Person> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email);
        
        Person person = await query.FirstOrDefaultAsync();

        return person;
    }

    /// <summary>
    /// Обновить сущность пользователя
    /// </summary>
    /// <param name="person"></param>
    public async Task UpdatePersonInfo(Person person)
    {
        Person currentPerson = await FindPersonAsync(person.Email);
        currentPerson = person;
        
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<Person> FindPersonAsync(string email, string password)
    {
        Person currentPerson = await _dbContext.Person
            .FirstOrDefaultAsync(x => x.Email == email);
        
        _assert.IsNull(currentPerson, $"Не найден пользователь с email: {email}");

        string hashPass = _registrationHelper.generateHashPass(password, currentPerson.Salt);
        
        Person person = await _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.HashPassword == hashPass)
            .FirstOrDefaultAsync();

        _assert.IsNull(person);

        return person;
    }
    
    /// <summary>
    /// Найти модель пользователя. Проверка на null
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<Person> FindPersonAsync(string email)
    {
        IQueryable<Person> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email);

        Person person = await query.FirstOrDefaultAsync();
        
        _assert.IsNull(person);

        return person;
    }

    /// <summary>
    /// Проверить уникальность почты
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns>True - пользователя с постой нет в системе</returns>
    public async Task<bool> CheckMailUniqueness(string email)
    {
        Person person = await GetPersonAsync(email);

        return person == null;
    }

    /// <summary>
    /// Добавить сущность пользователя
    /// </summary>
    /// <param name="newPerson">Пользователь</param>
    public async Task AddPerson(Person newPerson)
    {
        await AddModelAsync(newPerson);

        await SaveChangeAsync();
    }
}