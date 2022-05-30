using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Courses()
        {
            Client client = await GetAuthorize(HttpContext);

            if (client != null)
            {
                ViewData["Orders"] = await _context.Orders.Where(c => c.ClientId == client.Id).ToListAsync();
            }

            return View(await _context.Courses.Include(i => i.Image).ToListAsync());
        }

        public async Task<IActionResult> ShowCourse(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses.Include(c => c.Topics).Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

            if(course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> ShowTopic(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Topic topic = await _context.Topics.Include(l=>l.Lessons).Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

            if(topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        public async Task<IActionResult> ShowLesson(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Lesson lesson = await _context.Lessons.FindAsync(id);

            if(lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        public async Task<IActionResult> Pay(int id)
        {
            if (id != null)
            {
                Course course = await _context.Courses.FindAsync(id);

                if (course != null)
                {
                    ViewData["Course"] = course;
                    ViewData["Moth"] = new List<SelectListItem>
                    {
                        new SelectListItem{Text="01", Value="1" },
                        new SelectListItem{Text="02", Value="2" },
                        new SelectListItem{Text="03", Value="3" },
                        new SelectListItem{Text="04", Value="4" },
                        new SelectListItem{Text="05", Value="5" },
                        new SelectListItem{Text="06", Value="6" },
                        new SelectListItem{Text="07", Value="7" },
                        new SelectListItem{Text="08", Value="8" },
                        new SelectListItem{Text="09", Value="9" },
                        new SelectListItem{Text="10", Value="10" },
                        new SelectListItem{Text="11", Value="11" },
                        new SelectListItem{Text="12", Value="12" }
                    };
                    ViewData["Year"] = new List<SelectListItem>
                    {
                        new SelectListItem{Text="22", Value ="22"},
                        new SelectListItem{Text="23", Value ="23"},
                        new SelectListItem{Text="24", Value ="24"},
                        new SelectListItem{Text="25", Value ="25"},
                        new SelectListItem{Text="26", Value ="26"}
                    };
                    return View();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PayConfirmed(int id)
        {
            Client client = await GetAuthorize(HttpContext);

            if (id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            if (client != null)
            {
                Order order = new Order
                {
                    Client = client,
                    Course = course,
                    AccessId = 1
                };

                _context.Add(order);
                await _context.SaveChangesAsync();

                return View(await _context.Orders.Include(c => c.Client).Include(c => c.Course).FirstOrDefaultAsync(i => i.Id == order.Id));
            }

            return NotFound();
        }

        public async Task<VirtualFileResult> GetImage(int id)
        {
            if (id != null)
            {
                Course course = await _context.Courses.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

                if (course.Image != null)
                {
                    string content = "/Course/" + course.Id.ToString();
                    return File(Path.Combine("~", content, course.Image.FileName), course.Image.ContentType, course.Image.FileName);
                }

                return null;
            }

            return null;
        }

        private async Task<Client> GetAuthorize(HttpContext httpContext)
        {
            int idClient = Convert.ToInt32(User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType));

            return await _context.Clients.FindAsync(idClient);
        }
    }
}
