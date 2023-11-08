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
    public class AccessoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AccessoriesController(ApplicationDbContext context,
                                    UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET: Accessories
        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            string? userID = _userManager.GetUserId(User);
            IEnumerable<Accessory> accessories = await _context.Accessories.Where(a => a.AppUserId == userID)
                                                                           .ToListAsync();
            return View(accessories);
        }
        #endregion


        #region GET: Accessories/Details
        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessories
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        #endregion


        #region GET: Accessories/Create
        // GET: Accessories/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Accessories, "Id", "Name");
            return View();
        }

        #endregion


        #region POST: Accessories/Create
        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Accessory accessory)
        {
            ModelState.Remove("AppUserId");

            if (ModelState.IsValid)
            {
                accessory.AppUserId = _userManager.GetUserId(User);
                _context.Add(accessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", accessory.AppUserId);
            return View(accessory);
        }

        #endregion


        #region  GET: Accessories/Edit
        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            var accessory = await _context.Accessories.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == userId);
            if (accessory == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", accessory.AppUserId);
            return View(accessory);
        }

        #endregion


        #region POST: Accessories/Edit
        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AppUserId")] Accessory accessory)
        {
            if (id != accessory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoryExists(accessory.Id))
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
            string? userId = _userManager.GetUserId(User);

            ViewData["AppUserId"] = new SelectList(_context.Accessories.Where(a => a.AppUserId == userId), "Id", "Name", accessory.AppUserId);
            return View(accessory);
        }

        #endregion

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessories
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accessories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Accessories'  is null.");
            }
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory != null)
            {
                _context.Accessories.Remove(accessory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoryExists(int id)
        {
          return (_context.Accessories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
