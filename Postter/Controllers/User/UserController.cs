using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postter.Infrastructure.Context;

namespace Postter.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly AppDbContext _dbContext;
    
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
        if (user != null)
        {
            await Authenticate(model.Email); // аутентификация

            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            // добавляем пользователя в бд
            db.Users.Add(new User { Email = model.Email, Password = model.Password });
            await db.SaveChangesAsync();

            await Authenticate(model.Email); // аутентификация

            return RedirectToAction("Index", "Home");
        }
        else
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

        return View(model);
    }

    private async Task Authenticate(string userName)
    {
        // создаем один claim
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        };
        // создаем объект ClaimsIdentity
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}