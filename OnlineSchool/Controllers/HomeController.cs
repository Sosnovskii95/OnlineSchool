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

        public IActionResult Index()
        {
            var lessons = _context.Lessons.Where(i => i.Id <= 1).ToList();

            var test = _context.HintTestLessons.Where(c => c.ClientId == 1)
                .Where(v => v.ValueResult != null & 70 <= v.ValueResult).Where(i => i.LessonId <= 2).Select(s=>s.LessonId).Distinct().ToList();

            var s = test.Where(s => lessons.Select(i => i.Id).Contains(s)).Count();

            if (test.Where(s => lessons.Select(i => i.Id).Contains(s)).Count() > 0) ;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}