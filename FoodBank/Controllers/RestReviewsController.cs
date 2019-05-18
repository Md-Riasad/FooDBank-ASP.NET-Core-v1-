using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodBank.Data;
using FoodBank.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodBank.Controllers
{
    [Authorize]
    public class RestReviewsController : Controller
    { 
        private readonly ApplicationDbContext _context;

        public RestReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestReviews
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RestReview.Include(r => r.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RestReviews/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restReview = await _context.RestReview
                .Include(r => r.Restaurant)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (restReview == null)
            {
                return NotFound();
            }

            return View(restReview);
        }

        // GET: RestReviews/Create
       
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id");
            return View();
        }

        // POST: RestReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Rating,Body,ReviewerName,RestaurantId")] RestReview restReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id", restReview.RestaurantId);
            return View(restReview);
        }

        // GET: RestReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restReview = await _context.RestReview.SingleOrDefaultAsync(m => m.Id == id);
            if (restReview == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id", restReview.RestaurantId);
            return View(restReview);
        }

        // POST: RestReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Body,ReviewerName,RestaurantId")] RestReview restReview)
        {
            if (id != restReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestReviewExists(restReview.Id))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id", restReview.RestaurantId);
            return View(restReview);
        }

        // GET: RestReviews/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restReview = await _context.RestReview
                .Include(r => r.Restaurant)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (restReview == null)
            {
                return NotFound();
            }

            return View(restReview);
        }

        // POST: RestReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restReview = await _context.RestReview.SingleOrDefaultAsync(m => m.Id == id);
            _context.RestReview.Remove(restReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestReviewExists(int id)
        {
            return _context.RestReview.Any(e => e.Id == id);
        }
    }
}
