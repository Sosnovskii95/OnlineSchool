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
    public class OrdersController : Controller
    {
        private readonly DBContextSchool _context;

        public OrdersController(DBContextSchool context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var dBContextSchool = _context.Orders.Include(o => o.Access).Include(o => o.Client).Include(o => o.Course);
            return View(await dBContextSchool.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Access)
                .Include(o => o.Client)
                .Include(o => o.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AccessId"] = new SelectList(_context.Accesses, "Id", "TitleAccess");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,CourseId,UserId,StateOrderId,PayMethodId,AccessId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessId"] = new SelectList(_context.Accesses, "Id", "TitleAccess", order.AccessId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", order.CourseId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AccessId"] = new SelectList(_context.Accesses, "Id", "TitleAccess", order.AccessId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", order.CourseId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,CourseId,UserId,StateOrderId,PayMethodId,AccessId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["AccessId"] = new SelectList(_context.Accesses, "Id", "TitleAccess", order.AccessId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", order.ClientId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "TitleCourse", order.CourseId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Access)
                .Include(o => o.Client)
                .Include(o => o.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DBContextSchool.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
