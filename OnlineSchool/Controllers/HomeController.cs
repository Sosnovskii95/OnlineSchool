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
            HintTestLesson hintTestLesson = new HintTestLesson
            {
                ClientId = 1,
                NumberHint = await _context.HintTestLessons.Where(i => i.ClientId == 1).CountAsync() + 1
            };

            _context.Add(hintTestLesson);
            await _context.SaveChangesAsync();

            ViewData["Hint"] = hintTestLesson.Id;

            return View();
        }


        public async Task<IActionResult> TestLesson(int idLesson, int hint)
        {
            //Будем вытягивать из сесси


            List<ResultTestLesson> ResultTestLesson = await _context.ResultTestLessons.Where(i => i.HintTestLessonId == hint).Where(s => s.TestLesson.LessonId == idLesson).ToListAsync();

            if (ResultTestLesson != null)
            {

                TestLesson test = await _context.TestLessons.Where(s => !ResultTestLesson.Select(i => i.TestLessonId).Contains(s.Id)).FirstOrDefaultAsync();

                if (test == null)
                {
                    return RedirectToAction(nameof(ResultTest), new { idLesson = idLesson, hint = hint });
                }

                if (test != null)
                {
                    ViewData["IdLesson"] = idLesson;
                    ViewData["Hint"] = hint;
                    return View(test);
                }
            }
            else
            {
                return NotFound();
            }

            return null;
        }

        public async Task<IActionResult> CheckTest(int id, int idLesson, int hint, string answer)
        {
            //сделать обработку ответов
            if (id != null)
            {
                TestLesson test = await _context.TestLessons.FindAsync(id);

                if (test != null)
                {
                    ResultTestLesson resultTestLesson = new ResultTestLesson
                    {
                        HintTestLessonId = hint,
                        TestLesson = test,
                        ValueAnswer = answer
                    };

                    if (test.RightAnswer.Equals(answer))
                    {
                        resultTestLesson.ResultAnswer = true;

                        var hintTest = await _context.HintTestLessons.FindAsync(hint);

                        hintTest.CountRigth += 1;
                    }
                    else
                    {
                        resultTestLesson.ResultAnswer = false;
                    }

                    _context.Add(resultTestLesson);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(TestLesson), new { idLesson = idLesson, hint = hint });
                }
            }

            return View();
        }

        public async Task<IActionResult> ResultTest(int idLesson, int hint)
        {
            /*var hintTest = await _context.HintTestLessons.FirstOrDefaultAsync(i => i.Id == hint);

            int countAll = await _context.TestLessons.Where(i => i.LessonId == idLesson).CountAsync();

            hintTest.ValueResult = Convert.ToDouble(hintTest.CountRigth) / Convert.ToDouble(countAll) * 100;

            _context.Update(hintTest);
            await _context.SaveChangesAsync();

            ViewData["Progress"] = Convert.ToDouble(hintTest.CountRigth) / Convert.ToDouble(countAll) * 100;
            */
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}