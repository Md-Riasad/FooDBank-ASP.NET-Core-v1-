using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBank.Data;
using FoodBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodBank.Controllers
{ 
          [Authorize]
    public class ReviewsController : Controller
    {
         
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index([Bind(Prefix = "id")]int? restaurantId)
        {
            if (restaurantId == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.ToListAsync();

            if (restaurant != null)
            {

                restaurant = restaurant.Where(r => r.Id == restaurantId).ToList();    
               //return NotFound();
               
            }
             
            return View(restaurant);

        }
        // GET: RestReviews/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id");
            return View();
        }
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
        private IActionResult HttpNotFound()
        {
            throw new NullReferenceException();
        }

        // GET: RestReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restReview = await _context.RestReview.FindAsync(id);
            if (restReview == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Id", restReview.RestaurantId);
            return View(restReview);
        }

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

        private bool RestReviewExists(int id)
        {
            return _context.RestReview.Any(e => e.Id == id);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

    }
}