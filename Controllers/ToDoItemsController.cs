using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListMVC.Data;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    [Authorize]
    public class ToDoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ToDoItemsController(ApplicationDbContext context,
                                    UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET: ToDoItems
        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDoItems.Include(t => t.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        #endregion


        #region GET: ToDoItems/Details
        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            var toDoItem = await _context.ToDoItems
                                         .FirstOrDefaultAsync(c => c.Id == id && c.AppUserId == userId);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        #endregion


        #region GET: ToDoItems/Create
        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            string? userId = _userManager?.GetUserId(User);

            ViewData["AppUserId"] = new SelectList(_context.ToDoItems.Where(t => t.AppUserId == userId), "Id", "Name");
            return View();
        }

        #endregion


        #region POST: ToDoItems/Create
        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AppUserId,DateCreated,Completed")] ToDoItem toDoItem)
        {
            ModelState.Remove("AppUserId");
            string? userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                toDoItem.AppUserId = userId;
                toDoItem.DateCreated = DateTimeOffset.Now.ToUniversalTime();

                _context.Add(toDoItem);
                await _context.SaveChangesAsync();

                //foreach (var item in collection)
                //{

                //}

                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.ToDoItems.Where(t => t.AppUserId == userId), "Id", "Name");
            return View(toDoItem);
        }

        #endregion
        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            string? userId = _userManager?.GetUserId(User);
            var toDoItem = await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id && t.AppUserId == userId);
            if (toDoItem == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.ToDoItems.Where(t => t.AppUserId == userId), "Id", "Name");
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AppUserId,DateCreated,Completed")] ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.ToDoItems.Where(t => t.AppUserId == userId), "Id", "Name");
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDoItems'  is null.");
            }
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem != null)
            {
                _context.ToDoItems.Remove(toDoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
          return (_context.ToDoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
