using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineSchool.Data;
using OnlineSchool.Models.DBModel;
using System.Security.Claims;

namespace OnlineSchool.Controllers
{
    [Authorize(Roles = "client")]
    public class PersonalAreaController : Controller
    {
        private readonly DBContextSchool _context;

        public PersonalAreaController(DBContextSchool context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            Client client = await _context.Clients.FindAsync(Convert.ToInt32(User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType)));

            if (client != null)
            {
                return View(client);
            }

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Update(client);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        public async Task<IActionResult> History()
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType));
            if (id != null)
            {
                List<Order> orders = await _context.Orders.Where(c => c.ClientId == id)
                    .Include(a => a.Access)
                    .Include(c => c.Client)
                    .Include(c => c.Course)
                    .ToListAsync();
                return View(orders);
            }
            return View();
        }

        public async Task<IActionResult> IndexCourse()
        {
            return View(await _context.Courses.Include(i => i.Image).ToListAsync());
        }
    }
}
