using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CruDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CruDelicious.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Dish> DishList = _context.Dishes.ToList();
            ViewBag.DishList = DishList;
            return View("Index");
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost("Home/Create")]
        public IActionResult Create(Dish mydish)
        {
            if(ModelState.IsValid)
            {
                _context.Add(mydish);
                _context.SaveChanges();
                return RedirectToAction("Index");         
            }
                return View("New");
        }
        [HttpGet("Add")]
        public IActionResult Add()
        {
            
            return View("New");
        }
    

        [HttpGet("Home/Dish/{dishid}")]
        public IActionResult Show(int dishid)
        {
            List<Dish> onedish = _context.Dishes.Where(id => id.DishId == dishid).ToList();
            ViewBag.onedish = onedish;            

            return View("Show");
        }
        [HttpPost("Home/Dish/{dishid}/Delete")]
        public IActionResult Destroy(int dishid)
        {
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(id => id.DishId == dishid);
            _context.Dishes.Remove(RetrievedDish);
            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }
        [HttpPost("Home/Dish/{dishid}/Edit")]
        public IActionResult Edit(int dishid)
        {
            List<Dish> onedish = _context.Dishes.Where(id => id.DishId == dishid).ToList();
            ViewBag.onedish = onedish;            

            return View("Edit");
        }
        [HttpPost("Update")]
        public IActionResult Update(int dishid, string name, string chef, int tastiness, int calories, string description)
        {
            List<Dish> retrievedDish = _context.Dishes.Where(x => x.DishId == dishid).ToList();
            retrievedDish[0].Name=name;
            retrievedDish[0].Chef=chef;
            retrievedDish[0].Calories=calories;
            retrievedDish[0].Tastiness=tastiness;
            retrievedDish[0].Description=description;
            retrievedDish[0].UpdatedAt= DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
