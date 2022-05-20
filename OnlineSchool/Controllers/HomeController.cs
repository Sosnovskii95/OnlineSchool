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

        [HttpPost]
        public async Task<IActionResult> TestLesson(int idLesson)
        {
            if (idLesson != null)
            {
                Lesson lesson = await _context.Lessons.FindAsync(idLesson);

                if (lesson != null)
                {
                    var testLesson = await _context.TestLessons.Where(x => x.LessonId == lesson.Id).ToListAsync();

                    if (testLesson != null)
                    {
                        return View(testLesson);
                    }
                }
            }

            return null;
        }

        public async Task<IActionResult> ResultTest(List<string> AnswerOne, List<string> AnswerTwo,
            List<string> AnswerThree, List<string> AnswerFour, List<string> AnswerFive)
        {
            //сделать обработку ответов
            if (await GetResult(AnswerOne) != null)
            {
                await _context.AddRangeAsync(await GetResult(AnswerOne));
                await _context.SaveChangesAsync();
            }

            if(await GetResult(AnswerTwo) != null)
            {
                await _context.AddRangeAsync(await GetResult(AnswerTwo));
                await _context.SaveChangesAsync();
            }

            if(await GetResult(AnswerThree) != null)
            {
                await _context.AddRangeAsync(await GetResult(AnswerThree));
                await _context.SaveChangesAsync();
            }
            
            if(await GetResult(AnswerFour) != null)
            {
                await _context.AddRangeAsync(await GetResult(AnswerFour));
                await _context.SaveChangesAsync();
            }
            
            if(await GetResult(AnswerFive) != null)
            {
                await _context.AddRangeAsync(await GetResult(AnswerFive));
                await _context.SaveChangesAsync();
            }

            return View();



            /*List<ResultTestLesson> resultTestLessons = new List<ResultTestLesson>();

            foreach (var item in AnswerOne)
            {
                string[] idAndAnswer = item.Split(":");

                TestLesson testLesson = await _context.TestLessons.FindAsync(Convert.ToInt32(idAndAnswer[0]));

                if (testLesson != null)
                {
                    _context.Add(new ResultTestLesson
                    {
                        //позже добавить клиента (его айди)
                        TestLesson = testLesson,
                        ValueAnswer = idAndAnswer[1]
                    }
                    );
                    await _context.SaveChangesAsync();
                }
            }*/

            return null;
        }

        private async Task<List<ResultTestLesson>> GetResult(List<string> answers)
        {
            List<ResultTestLesson> resultTestLessons = answers.Count > 0 ? new List<ResultTestLesson>() : null;

            if(resultTestLessons == null)
            {
                return null;
            }

            foreach(var item in answers)
            {
                string[] idAndAnswer = item.Split(":");

                TestLesson testLesson = await _context.TestLessons.FindAsync(Convert.ToInt32(idAndAnswer[0]));

                if (testLesson != null)
                {
                    resultTestLessons.Add(new ResultTestLesson
                    {
                        //позже добавить клиента (его айди)
                        TestLesson = testLesson,
                        ValueAnswer = idAndAnswer[1]
                    });
                }
            }

            return resultTestLessons;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}