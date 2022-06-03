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

        public async Task<IActionResult> Index()
        {
            Client client = await GetAuthorize(HttpContext);

            if (client != null)
            {
                var hintLesson = await _context.HintTestLessons.Where(i => i.ClientId == client.Id)
                    .Where(j => j.ValueResult != null & 70 <= j.ValueResult)
                    .Include(t => t.ResultTestLessons)
                    .ThenInclude(s => s.TestLesson)
                    .ThenInclude(s => s.Lesson)
                    .ThenInclude(c => c.Topic)
                    .ThenInclude(c => c.Course)
                    .ToListAsync();

                return View(hintLesson);
            }

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
            if (id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses.Include(c => c.Topics).Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> ShowTopic(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Topic topic = await _context.Topics.Include(l => l.Lessons).Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        public async Task<IActionResult> ShowLesson(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.Include(t => t.TestLessons).FirstOrDefaultAsync(i => i.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        public async Task<IActionResult> RunTest(int idLesson, int? hint)
        {
            Client client = await GetAuthorize(HttpContext);

            if (client == null)
            {
                return NotFound();
            }

            HintTestLesson hintTestLesson;

            if (hint.HasValue)
            {
                hintTestLesson = await _context.HintTestLessons.Where(c => c.ClientId == client.Id).Where(h => h.Id == hint.Value).FirstOrDefaultAsync();
            }
            else
            {
                hintTestLesson = new HintTestLesson
                {
                    ClientId = client.Id,
                    NumberHint = await _context.HintTestLessons.Where(i => i.ClientId == 1).CountAsync() + 1
                };
                _context.Add(hintTestLesson);
                await _context.SaveChangesAsync();
            }

            var result = await _context.ResultTestLessons.Where(h => h.HintTestLessonId == hintTestLesson.Id).ToListAsync();

            if (result != null)
            {
                TestLesson test = await _context.TestLessons.Where(s => !result.Select(i => i.TestLessonId).Contains(s.Id)).Where(j => j.LessonId == idLesson).FirstOrDefaultAsync();

                if (test == null)
                {
                    return RedirectToAction(nameof(ResultTest), new { idLesson = idLesson, hint = hintTestLesson.Id });
                }

                if (test != null)
                {
                    ViewData["IdLesson"] = idLesson;
                    ViewData["Hint"] = hintTestLesson.Id;
                    return View(test);
                }
            }

            return null;
        }

        public async Task<IActionResult> CheckTest(int id, int idLesson, int hintId, string answer)
        {
            //сделать обработку ответов
            if (id != null)
            {
                TestLesson test = await _context.TestLessons.FindAsync(id);

                if (test != null)
                {
                    ResultTestLesson resultTestLesson = new ResultTestLesson
                    {
                        HintTestLessonId = hintId,
                        TestLesson = test,
                        ValueAnswer = answer
                    };

                    if (test.RightAnswer.Equals(answer))
                    {
                        resultTestLesson.ResultAnswer = true;

                        var hintTest = await _context.HintTestLessons.FindAsync(hintId);

                        hintTest.CountRigth += 1;
                    }
                    else
                    {
                        resultTestLesson.ResultAnswer = false;
                    }

                    _context.Add(resultTestLesson);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(RunTest), new { idLesson = idLesson, hint = hintId });
                }
            }

            return View();
        }


        public async Task<IActionResult> ResultTest(int idLesson, int hint)
        {
            var hintTest = await _context.HintTestLessons.FirstOrDefaultAsync(i => i.Id == hint);

            int countAll = await _context.TestLessons.Where(i => i.LessonId == idLesson).CountAsync();

            int valueResult = Convert.ToInt32(Math.Round(Convert.ToDouble(hintTest.CountRigth) / Convert.ToDouble(countAll) * 100));

            if (valueResult > 0)
            {
                hintTest.ValueResult = valueResult;
                _context.Update(hintTest);
                await _context.SaveChangesAsync();
            }
            ViewData["Progress"] = valueResult;
            ViewData["IdLesson"] = idLesson;

            return View();
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

        public async Task<VirtualFileResult> GetImage(int id, string typeImage)
        {
            if (id != null)
            {
                switch (typeImage)
                {
                    case "course":
                        {
                            Course course = await _context.Courses.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

                            if (course.Image != null)
                            {
                                string content = "/Course/" + course.Id.ToString();
                                return File(Path.Combine("~", content, course.Image.FileName), course.Image.ContentType, course.Image.FileName);
                            }
                            else
                            {
                                string current = "/";
                                return File(Path.Combine("~" + current, "noimage.jpg"), "image/jpeg", "noimage.jpg");
                            }
                            break;
                        }
                    case "topic":
                        {
                            Topic topic = await _context.Topics.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);

                            if (topic.Image != null)
                            {
                                string content = "/Topic/" + topic.Id.ToString();
                                return File(Path.Combine("~", content, topic.Image.FileName), topic.Image.ContentType, topic.Image.FileName);
                            }
                            else
                            {
                                string current = "/";
                                return File(Path.Combine("~" + current, "noimage.jpg"), "image/jpeg", "noimage.jpg");
                            }
                            break;
                        }
                }
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
