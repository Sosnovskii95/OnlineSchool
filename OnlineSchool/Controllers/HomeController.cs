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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            ViewData["Lesson"] = new SelectList(_context.Lessons, "Id", "TitleLesson");
            return View();
        }


        public async Task<IActionResult> TestLesson(int idLesson)
        {
            //Будем вытягивать из сессии
            int idClient = 1;

            List<ResultTestLesson> ResultTestLesson = await _context.ResultTestLessons.Where(i => i.ClientId == idClient).Where(s => s.TestLesson.LessonId == idLesson).ToListAsync();

            if (ResultTestLesson !=null)
            {

                TestLesson test = await _context.TestLessons.Where(s => !ResultTestLesson.Select(i => i.TestLessonId).Contains(s.Id)).FirstOrDefaultAsync();

                if(test == null)
                {
                    return RedirectToAction(nameof(ResultTest), new { idLesson = idLesson });
                }

                if (test != null)
                {
                    ViewData["IdLesson"] = idLesson;
                    return View(test);
                }
            }
            else
            {
                return NotFound();
            }

            return null;
        }

        public async Task<IActionResult> CheckTest(int id, int idLesson, string answer)
        {
            //сделать обработку ответов
            if (id != null)
            {
                TestLesson test = await _context.TestLessons.FindAsync(id);

                if (test != null)
                {
                    _context.Add(new ResultTestLesson
                    {
                        ClientId = 1,
                        TestLesson = test,
                        ValueAnswer = answer,
                    });

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(TestLesson), new { idLesson = idLesson });
                }
            }

            return View();
        }

        public async Task<IActionResult> ResultTest(int idLesson)
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