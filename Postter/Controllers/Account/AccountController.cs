﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers;
using Postter.Common.Helpers.ApiResponse;
using Postter.Controllers.Account.Models;
using Postter.UseCases.UseCaseAccount;

namespace Postter.Controllers.Account;

/// <summary>
/// Контроллер аккаунтов
/// </summary>
[ApiController]
[Route("[controller]")]
public class AccountController : CustomController
{
    public AccountController(IUseCaseAccount useCaseAccount)
    {
        _useCaseAccount = useCaseAccount;
    }

    private readonly IUseCaseAccount _useCaseAccount;


    /// <summary>
    /// Получить токен аутентификации
    /// </summary>
    /// <param name="email">Почта</param>
    /// <param name="password">Пароль</param>
    [HttpGet("/token")]
    [AllowAnonymous]
    public async Task<IActionResult> Token(string email, string password)
    {
        ClaimsIdentity identity = await _useCaseAccount.GetIdentity(email, password);
        if (identity == null)
            return BadRequest("Неверная почта или пароль.");

        JwtSecurityToken token = _useCaseAccount.GetToken(identity);
        string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name,
            accountId = identity.GetUserId()
        };

        return Ok(response);
    }
    
    /// <summary>
    /// Получить модель пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/account")]
    [CustomAuthorize]
    public async Task<AccountUi> GetPersonUiAsync(Guid id) =>
        await _useCaseAccount.GetPersonUiAsync(id);

    /// <summary>
    /// Получить свою модель пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet("/self-account")]
    [CustomAuthorize]
    public async Task<AccountUi> GetPersonUiAsync()
    {
        Guid id = new Guid(HttpContext.User.Identity.GetUserId());
        
        return await _useCaseAccount.GetPersonUiAsync(id);
    }

    /// <summary>
    /// Зарегистрировать нового пользователя
    /// </summary>
    /// <param name="model">RegistrationModel</param>
    /// <returns></returns>
    [HttpPost("/registration")]
    [AllowAnonymous]
    public async Task<IActionResult> Registration(RegistrationModel model)
    {
        var response = new
        {
            account = await _useCaseAccount.Registration(model),
            token = GetTokenByIdentity(model.Email, model.Password)
        };

        return Ok(response);
    }

    /// <summary>
    /// Удалить аккаунт
    /// </summary>
    /// <param name="accountId">Id пользователя</param>
    /// <returns></returns>
    [HttpDelete("/delete-account")]
    [CustomAuthorize(RolesEnum.Admin, RolesEnum.Moder)]
    public async Task<IActionResult> DeleteAccount(Guid accountId)
    {
        await _useCaseAccount.DeleteAccount(accountId);

        return Ok();
    }

    /// <summary>
    /// Обновить базовую информацию о пользователе
    /// </summary>
    /// <param name="model">UpdateAccountInfoModel</param>
    /// <returns></returns>
    [HttpPatch("/update-account-info")]
    [CustomAuthorize]
    public async Task<IActionResult> UpdateAccountInfo(UpdateAccountInfoModel model)
    {
        model.Id = new Guid(HttpContext.User.Identity.GetUserId());
        await _useCaseAccount.UpdateAccountInfo(model);

        return Ok();
    }

    /// <summary>
    /// Выдать роль пользователю 
    /// </summary>
    /// <param name="email">Почта пользователя</param>
    /// <param name="role">необходимая роль</param>
    [HttpPost("/give-the-user-a-role")]
    [CustomAuthorize(RolesEnum.Admin)]
    public async Task<IActionResult> GiveTheUserARole(string email, string role)
    {
        await _useCaseAccount.GiveTheUserARole(email, role);

        return Ok();
    }

    private string GetTokenByIdentity(string email, string password)
    {
        ClaimsIdentity identity = _useCaseAccount.GetIdentity(email, password).Result;
        JwtSecurityToken token = _useCaseAccount.GetToken(identity);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}