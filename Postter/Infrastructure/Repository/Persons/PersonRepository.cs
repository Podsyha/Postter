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
    /// Получить DTO модель пользователя. Без првоерки на null
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="email">Почта</param>
    /// <returns></returns>
    public async Task<Person> GetPersonAsync(string password, string email)
    {
        IQueryable<Person> query = _dbContext.Person
            .AsNoTracking()
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.Password == password);

        Person person = await query.FirstOrDefaultAsync();

        return person;
    }

    /// <summary>
    /// Найти DTO модель пользователя. Проверка на null
    /// </summary>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<Person> FindPersonAsync(string password, string email)
    {
        IQueryable<Person> query = _dbContext.Person
            .AsNoTracking()
            .Include(x => x.Role)
            .Where(person => person.Email == email && person.Password == password);

        Person person = await query.FirstOrDefaultAsync();
        
        _assert.IsNull(person);

        return person;
    }
}