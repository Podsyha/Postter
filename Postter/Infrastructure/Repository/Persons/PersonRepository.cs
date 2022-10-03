using Microsoft.EntityFrameworkCore;
using Postter.Common.Assert;
using Postter.Infrastructure.Context;
using Postter.Infrastructure.DAO;

namespace Postter.Infrastructure.Repository.Persons;

public class PersonRepository : AppDbFunc, IPersonRepository
{
    public PersonRepository(AppDbContext dbContext, IAssert assert) : base(dbContext, assert)
    {
        _assert = assert;
    }

    private readonly IAssert _assert;


    /// <summary>
    /// Получить модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<Person> GetPersonAsync(string email, string password)
    {
        IQueryable<Person> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.Password == password);
        
        Person person = await query.FirstOrDefaultAsync();

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
        IQueryable<Person> query = _dbContext.Person
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.Password == password);

        Person person = await query.FirstOrDefaultAsync();
        
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
}