using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Linq;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
        // استدعيناها مع كائن
        private readonly ApplicationDbContext _context;         

        // Constrecture دالة بناء 
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateNewRecord(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
				_context.hotel.Add(hotels);
				_context.SaveChanges();
				return RedirectToAction("Index");

			}
			var hotel= _context.hotel.ToList();
            return View("Index", hotel);
         
        }


        public IActionResult Delete( int Id)
        { 
            var hoteldelete=_context.hotel.SingleOrDefault(x => x.Id == Id);
            _context.hotel.Remove(hoteldelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit"); 
        }


        public IActionResult Edit(int Id)
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == Id);
            return View(hoteledit);
        }

        // overload 

        [HttpPost]
        public IActionResult Index(string city)
        {
            var hotel = _context.hotel.Where(x => x.City.Equals(city));
            return View(hotel);

        }




        public IActionResult Index()
        {
            var hotel= _context.hotel.ToList();
            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}