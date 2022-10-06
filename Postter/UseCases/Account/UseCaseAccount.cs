using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Assert;
using Postter.Common.Auth;
using Postter.Common.Helpers;
using Postter.Controllers.Account.Models;
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
    /// <param name="model">RegistrationModel</param>
    public async Task Registration(RegistrationModel model)
    {
        bool isUniquenessEmail = await _personRepository.CheckMailUniqueness(model.Email);
        _assert.ThrowIfFalse(isUniquenessEmail, "Пользователь с данной почтой уже существует в системе");

        RegistrationHelper helper = new();
        string salt = helper.generateSalt();
        string hashPass = helper.generateHashPass(model.Password, salt);

        AccountEntity newAccountEntity = new()
        {
            Email = model.Email,
            About = model.About,
            Name = model.Name,
            HashPassword = hashPass,
            Salt = salt,
            RoleId = (int)RolesEnum.User
        };

        await _personRepository.AddPerson(newAccountEntity);
    }

    /// <summary>
    /// Удалить аккаунт
    /// </summary>
    /// <param name="accountId">Id пользователя</param>
    /// <returns></returns>
    public async Task DeleteAccount(Guid accountId) =>
        await _personRepository.DeleteAccount(accountId);
    
    /// <summary>
    /// Обновить базовую информацию о пользователе
    /// </summary>
    /// <param name="model">UpdateAccountInfoModel</param>
    /// <returns></returns>
    public async Task UpdateAccountInfo(UpdateAccountInfoModel model)
    {
        AccountEntity accountEntity = await _personRepository.FindPersonAsync(model.Id);

        accountEntity.About = model.About;
        accountEntity.Name = model.Name;
        
        await _personRepository.UpdatePersonInfo(accountEntity);
    }

    /// <summary>
    /// Получить токен аутентификации
    /// </summary>
    /// <param name="identity">Claim</param>
    /// <returns></returns>
    public JwtSecurityToken GetToken(ClaimsIdentity identity)
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
        AccountEntity accountEntity = await _personRepository.GetPersonAsync(email, password);

        if (accountEntity == null) return null;

        List<Claim> claims = new()
        {
            new(ClaimsIdentity.DefaultNameClaimType, accountEntity.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, accountEntity.Role.Name),
            new(ClaimTypes.NameIdentifier, accountEntity.Id.ToString())
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
        AccountEntity accountEntity = await _personRepository.GetPersonAsync(email);

        _assert.IsNull(accountEntity, $"Не найден пользователь с email: {email}");

        List<RoleEntity> roles = await _roleRepository.GetAllRoles();
        RoleEntity currentRoleEntity = roles.FirstOrDefault(x => x.Name == role);

        _assert.IsNull(currentRoleEntity, $"Не найдена роль: {role}");

        accountEntity.RoleId = currentRoleEntity.Id;
        await _personRepository.UpdatePersonInfo(accountEntity);
    }
}