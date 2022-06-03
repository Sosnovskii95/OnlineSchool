using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Models.AccountModel;
using OnlineSchool.Models.DBModel;
using OnlineSchool.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace OnlineSchool.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContextSchool _context;

        public AccountController(DBContextSchool context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (loginModel.InvateAdmin)
                {
                    User user = await _context.Users.Where(e => e.EmailUser.Equals(loginModel.Email) && e.PasswordUser.Equals(loginModel.Password)).Include(r => r.Role).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        await Authenticate(user.Id, user.Role.ValueRole);

                        switch (user.Role.ValueRole)
                        {
                            case "admin": return RedirectToAction(nameof(Index), "Users");
                            case "teacher": return RedirectToAction(nameof(Index), "Lessons");
                        }

                        return null;
                    }

                    ModelState.AddModelError("", "Пользователя с такими данными не существует");
                    return View(loginModel);
                }
                else
                {
                    Client client = await _context.Clients.Where(e => e.EmailClient.Equals(loginModel.Email) && e.PasswordClient.Equals(loginModel.Password)).FirstOrDefaultAsync();

                    if (client != null)
                    {
                        await Authenticate(client.Id, "client");

                        return RedirectToAction(nameof(Index), "PersonalArea");
                    }

                    ModelState.AddModelError("", "Клиента с такими данными не сущестует");
                    return View(loginModel);
                }
            }

            return View(loginModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var email = await _context.Clients.FirstOrDefaultAsync(e => e.EmailClient.Equals(registerModel.EmailClient));

                if (email != null)
                {
                    ModelState.AddModelError("EmailClient", "Email занят");
                    return View(registerModel);
                }

                var phone = await _context.Clients.FirstOrDefaultAsync(e => e.NumberPhone.Equals(registerModel.NumberPhone));

                if (phone != null)
                {
                    ModelState.AddModelError("NumberPhone", "Телефон уже используется");
                    return View(registerModel);
                }

                Client client = new Client
                {
                    EmailClient = registerModel.EmailClient,
                    FirstLastNameClient = registerModel.FirstLastNameClient,
                    NumberPhone = registerModel.NumberPhone,
                    PasswordClient = registerModel.PasswordClient,
                    Age = registerModel.Age
                };

                _context.Add(client);
                await _context.SaveChangesAsync();

                await Authenticate(client.Id, "client");

                return null;
            }

            return View(registerModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        private async Task Authenticate(int idClientUser, string titleRole)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, idClientUser.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, titleRole)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}