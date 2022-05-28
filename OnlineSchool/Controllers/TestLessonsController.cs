using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineSchool.Data;
using OnlineSchool.Models.DBModel;

namespace OnlineSchool.Controllers
{
    public class TestLessonsController : Controller
    {
        private readonly DBContextSchool _context;

        public TestLessonsController(DBContextSchool context)
        {
            _context = context;
        }

        // GET: TestLessons
        public async Task<IActionResult> Index()
        {
            var dBContextSchool = _context.TestLessons.Include(t => t.Lesson);
            return View(await dBContextSchool.ToListAsync());
        }

        // GET: TestLessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestLessons == null)
            {
                return NotFound();
            }

            var testLesson = await _context.TestLessons
                .Include(t => t.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testLesson == null)
            {
                return NotFound();
            }

            return View(testLesson);
        }

        // GET: TestLessons/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "TitleLesson");
            return View();
        }

        // POST: TestLessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LessonId,Question,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,AnswerFive,RightAnswer,HintQuestion")] TestLesson testLesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testLesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "TitleLesson", testLesson.LessonId);
            return View(testLesson);
        }

        // GET: TestLessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestLessons == null)
            {
                return NotFound();
            }

            var testLesson = await _context.TestLessons.FindAsync(id);
            if (testLesson == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "TitleLesson", testLesson.LessonId);
            return View(testLesson);
        }

        // POST: TestLessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LessonId,Question,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,AnswerFive,RightAnswer,HintQuestion")] TestLesson testLesson)
        {
            if (id != testLesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testLesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestLessonExists(testLesson.Id))
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
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "TitleLesson", testLesson.LessonId);
            return View(testLesson);
        }

        // GET: TestLessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestLessons == null)
            {
                return NotFound();
            }

            var testLesson = await _context.TestLessons
                .Include(t => t.Lesson)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testLesson == null)
            {
                return NotFound();
            }

            return View(testLesson);
        }

        // POST: TestLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestLessons == null)
            {
                return Problem("Entity set 'DBContextSchool.TestLessons'  is null.");
            }
            var testLesson = await _context.TestLessons.FindAsync(id);
            if (testLesson != null)
            {
                _context.TestLessons.Remove(testLesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestLessonExists(int id)
        {
          return (_context.TestLessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
