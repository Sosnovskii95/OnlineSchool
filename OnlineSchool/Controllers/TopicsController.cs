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
    public class TopicsController : Controller
    {
        private readonly DBContextSchool _context;
        private readonly IWebHostEnvironment _webHost;

        public TopicsController(DBContextSchool context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var dBContextSchool = _context.Topics.Include(t => t.Course).Include(t => t.Image);
            return View(await dBContextSchool.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Course)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse");
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleTopic,DescriptionTopic,CourseId")] Topic topic, IFormFile? nameFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();

                if (nameFile != null)
                {
                    Image image = new Image
                    {
                        FileName = nameFile.FileName,
                        ContentType = nameFile.ContentType
                    };
                    _context.Add(image);

                    string pathImage = _webHost.WebRootPath + "/Topic/" + topic.Id.ToString();
                    if (!Directory.Exists(pathImage))
                    {
                        Directory.CreateDirectory(pathImage);
                    }

                    pathImage += "/" + nameFile.FileName;

                    using (var fileStream = new FileStream(pathImage, FileMode.Create))
                    {
                        await nameFile.CopyToAsync(fileStream);
                    }

                    topic.Image = image;
                    _context.Update(topic);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", topic.CourseId);
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", topic.CourseId);
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleTopic,DescriptionTopic,CourseId")] Topic topic, IFormFile? nameFile)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Topic updateTopic = await _context.Topics.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == topic.Id);

                    updateTopic.DescriptionTopic = topic.DescriptionTopic;
                    updateTopic.CourseId = topic.CourseId;
                    updateTopic.TitleTopic = topic.TitleTopic;

                    if (nameFile != null)
                    {
                        if (updateTopic.Image != null)
                        {
                            DeleteFiles(updateTopic.Id, _webHost.WebRootPath + "/Topic", updateTopic.Image.FileName);
                            _context.Remove(updateTopic.Image);
                            await _context.SaveChangesAsync();
                        }

                        Image image = new Image
                        {
                            FileName = nameFile.FileName,
                            ContentType = nameFile.ContentType
                        };

                        string pathImage = _webHost.WebRootPath + "/Topic/" + updateTopic.Id.ToString();
                        if (!Directory.Exists(pathImage))
                        {
                            Directory.CreateDirectory(pathImage);
                        }

                        pathImage += "/" + nameFile.FileName;

                        using (var fileStream = new FileStream(pathImage, FileMode.Create))
                        {
                            await nameFile.CopyToAsync(fileStream);
                        }

                        _context.Add(image);
                        updateTopic.Image = image;
                    }

                    _context.Update(updateTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", topic.CourseId);
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Course)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topics == null)
            {
                return Problem("Entity set 'DBContextSchool.Topics'  is null.");
            }
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
            return (_context.Topics?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<VirtualFileResult> GetImage(int id)
        {
            if (id != null)
            {
                Topic topic = await _context.Topics.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                if (topic != null)
                {
                    string current = "/Topic/" + id.ToString();
                    return File(Path.Combine("~" + current, topic.Image.FileName), topic.Image.ContentType, topic.Image.FileName);
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
                Topic topic = await _context.Topics.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                if (topic != null)
                {
                    DeleteFiles(topic.Id, _webHost.WebRootPath + "/Topic", topic.Image.FileName);

                    _context.Remove(topic.Image);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Edit), new { id = id });
        }

        private void DeleteFiles(int idFolderFiles, string nameFolderFiles, string deleteNameFile)
        {
            string pathFiles = nameFolderFiles + "/" + idFolderFiles;
            System.IO.File.Delete(pathFiles + "/" + deleteNameFile);
        }
    }
}
