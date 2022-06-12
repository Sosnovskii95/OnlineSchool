using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Models;
using System.Diagnostics;
using OnlineSchool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineSchool.Models.DBModel;

namespace OnlineSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContextSchool _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DBContextSchool context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<VirtualFileResult> GetImage(int id)
        {
            if (id != null)
            {
                Course course = await _context.Courses.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                if (course.Image != null)
                {
                    string current = "/Course/" + id.ToString();
                    return File(Path.Combine("~" + current, course.Image.FileName), course.Image.ContentType, course.Image.FileName);
                }
                else
                {
                    string current = "/";
                    return File(Path.Combine("~" + current, "noimage.jpg"), "image/jpeg", "noimage.jpg");
                }
            }
            else
            {
                return null;
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}