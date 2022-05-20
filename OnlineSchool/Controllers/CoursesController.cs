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
    public class CoursesController : Controller
    {
        private readonly DBContextSchool _context;
        private readonly IWebHostEnvironment _webHost;

        public CoursesController(DBContextSchool context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleCourse,DescriptionCourse")] Course course, IFormFile ImageFileName)
        {
            if (ModelState.IsValid)
            {
                course.ImageFileName = ImageFileName.FileName;
                course.ContentTypeFileName = ImageFileName.ContentType;
                _context.Add(course);
                await _context.SaveChangesAsync();

                string pathImage = _webHost.WebRootPath + "/Course/" + course.Id.ToString();
                if (!Directory.Exists(pathImage))
                {
                    Directory.CreateDirectory(pathImage);
                }

                pathImage += "/" + ImageFileName.FileName;

                using (var fileStream = new FileStream(pathImage, FileMode.Create))
                {
                    await ImageFileName.CopyToAsync(fileStream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleCourse,DescriptionCourse,ImageFileName")] Course course, IFormFile ImageFileName)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    course.ImageFileName = ImageFileName.FileName;
                    course.ContentTypeFileName = ImageFileName.ContentType;

                    string pathImage = _webHost.WebRootPath + "/Course/" + course.Id.ToString();
                    if (!Directory.Exists(pathImage))
                    {
                        Directory.CreateDirectory(pathImage);
                    }

                    pathImage += "/" + ImageFileName.FileName;

                    using (var fileStream = new FileStream(pathImage, FileMode.Create))
                    {
                        await ImageFileName.CopyToAsync(fileStream);
                    }

                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'DBContextSchool.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<VirtualFileResult> GetImage(int id)
        {
            if (id != null)
            {
                Course course = await _context.Courses.FindAsync(id);
                if (course != null)
                {
                    string current = "/Course/"+id.ToString();
                    return File(Path.Combine("~" + current, course.ImageFileName), course.ContentTypeFileName, course.ImageFileName);
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (id != null)
            {
                Course course = await _context.Courses.FindAsync(id);
                if (course != null)
                {
                    DeleteFiles(course.Id, _webHost.WebRootPath + "/Course", course.ImageFileName);

                    course.ImageFileName = "";
                    course.ContentTypeFileName = "";
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Edit), new { id = id });
        }

        public void DeleteFiles(int idFolderFiles, string nameFolderFiles, string deleteNameFile)
        {
            string pathFiles = nameFolderFiles + "/" + idFolderFiles;
            System.IO.File.Delete(pathFiles + "/" + deleteNameFile);
        }
    }
}
