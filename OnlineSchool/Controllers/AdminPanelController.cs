using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineSchool.Data;
using OnlineSchool.Models.DBModel;
using System.Security.Claims;

namespace OnlineSchool.Controllers
{
    [Authorize(Roles = "admin, teacher, metodist")]
    public class AdminPanelController : Controller
    {
        private readonly DBContextSchool _context;

        public AdminPanelController(DBContextSchool context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            User user = await GetAuthorize(HttpContext);

            if (user != null)
            {
                return View(await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(i => i.Id == user.Id));
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(r => r.Id == user.RoleId), "Id", "TitleRole", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmailUser,LoginUser,PasswordUser,FullNameUser, ActiveUser,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(r => r.Id == user.RoleId), "Id", "TitleRole", user.RoleId);
            return View(user);
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<User> GetAuthorize(HttpContext httpContext)
        {
            int idUser = Convert.ToInt32(User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType));

            return await _context.Users.FindAsync(idUser);
        }
    }
}
