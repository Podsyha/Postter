using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Assert;
using Postter.Common.Auth;
using Postter.Common.Helpers;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
using Postter.Infrastructure.Repository.Persons;
using Postter.Infrastructure.Repository.Roles;

namespace Postter.UseCases.Account;

public class UseCaseAccount : IUseCaseAccount
{
    public UseCaseAccount(IPersonRepository personRepository, IAssert assert, IRoleRepository roleRepository)
    {
        _personRepository = personRepository;
        _assert = assert;
        _roleRepository = roleRepository;
    }
    
    private readonly IPersonRepository _personRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAssert _assert;

    
    /// <summary>
    /// Зарегистрировать нового пользователя
    /// </summary>
    /// <param name="email">Почта</param>
    /// <param name="password">Пароль</param>
    public async Task Register(string email, string password)
    {
        bool isUniquenessEmail = await _personRepository.CheckMailUniqueness(email);
        _assert.ThrowIfFalse(isUniquenessEmail, "Пользователь с данной почтой уже есть в системе");
        
        RegistrationHelper helper = new();
        string salt = helper.generateSalt();
        string hashPass = helper.generateHashPass(password, salt);

        Person newPerson = new()
        {
            Email = email,
            HashPassword = hashPass,
            Salt = salt,
            RoleId = (int)RolesEnum.User
        };

        await _personRepository.AddPerson(newPerson);
    }
    
    /// <summary>
    /// Получить токен аутентификации
    /// </summary>
    /// <param name="email">Почта</param>
    /// <param name="identity">Claim</param>
    /// <returns></returns>
    public JwtSecurityToken GetToken(string email, ClaimsIdentity identity)
    {
        DateTime now = DateTime.UtcNow;
        
        JwtSecurityToken jwt = new(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return jwt;
    }
    
    /// <summary>
    /// Получить claim пользователя
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        Person person = await _personRepository.GetPersonAsync(email,password);

        if (person == null) return null;

        List<Claim> claims = new()
        {
            new(ClaimsIdentity.DefaultNameClaimType, person.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
        };

        ClaimsIdentity claimsIdentity =
            new(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
        
        return claimsIdentity;
    }
    
    /// <summary>
    /// Выдать роль пользователю
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <param name="role">необходимая роль</param>
    public async Task GiveTheUserARole(string email, string role)
    {
        Person person = await _personRepository.GetPersonAsync(email);
        
        _assert.IsNull(person, $"Не найден пользователь с email: {email}");

        List<Role> roles = await _roleRepository.GetAllRoles();
        Role currentRole = roles.FirstOrDefault(x => x.Name == role);
        
        _assert.IsNull(currentRole, $"Не найдена роль: {role}");
        
        person.RoleId = currentRole.Id;
        await _personRepository.UpdatePersonInfo(person);
    }
}