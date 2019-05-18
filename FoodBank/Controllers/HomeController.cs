using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodBank.Models;
using FoodBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FoodBank.Controllers
{      [Authorize]
    public class HomeController : Controller
    {


        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchstr ,int page=1)
        {
            IQueryable<Restaurant> model =  _context.Restaurant;
            ViewBag.stext = searchstr;
            if (searchstr != null)
            {
                searchstr = searchstr.ToUpper();
                model = model.Where(r => r.Name.ToUpper().Contains(searchstr));

            }
            if (page <= 0) { page = 1; }
            int pageSize = 5;
            return View(await PaginatedList<Restaurant>.CreateAsync(model ,page,pageSize));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
