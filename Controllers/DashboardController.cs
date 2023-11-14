using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(string city)
        {
            var hotel = _context.hotel.Where(x => x.City.Equals(city));
            return View(hotel);

        }

        [Authorize]
        public IActionResult Index()
        {
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.CurrentUser= currentuser;
            // CookieOptions option = new CookieOptions();
            // option.Expires = DateTime.Now.AddMinutes(20);
            // Response.Cookies.Append("UserName", currentuser, option);
          
            HttpContext.Session.SetString("UserName",currentuser);
			var hotel = _context.hotel.ToList();
			return View(hotel);
	
        }


		public IActionResult Rooms()
		{
            var hotel=_context.hotel.ToList();  
            ViewBag.hotel=hotel;
            //ViewBag.CurrentUser = Request.Cookies["UserName"];
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");

			var rooms = _context.rooms.ToList();
			return View(rooms);

		}
		public IActionResult CreateNewRooms(Rooms rooms)
		{
			_context.rooms.Add(rooms);
			_context.SaveChanges();
			return RedirectToAction("Rooms");

		}

        public IActionResult DeleteRooms(int Id)
        {
            var hotelDelete = _context.rooms.SingleOrDefault(x => x.Id == Id);
            if (hotelDelete != null)
            {
                _context.rooms.Remove(hotelDelete);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("Rooms");
        }



        public IActionResult RoomDetails()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			var rooms = _context.rooms.ToList();
			ViewBag.rooms = rooms;
			//ViewBag.CurrentUser = Request.Cookies["UserName"];
			ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
			var roomDetails = _context.roomDetails.ToList();
			return View(roomDetails);

		}


		public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
		{
			_context.roomDetails.Add(roomDetails);
			_context.SaveChanges();
			return RedirectToAction("RoomDetails");

		}
		public IActionResult DeleteRoomDetails(int Id)
		{
			var hotelDeleteDetails = _context.roomDetails.SingleOrDefault(x => x.Id == Id);
			if (hotelDeleteDetails != null)
			{
				_context.roomDetails.Remove(hotelDeleteDetails);
				_context.SaveChanges();
				TempData["Del"] = "Ok";
			}
			return RedirectToAction("RoomDetails");
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
            var hotelEdit = _context.hotel.SingleOrDefault(x => x.Id == Id);
            return View(hotelEdit);
        }



        public IActionResult Delete(int Id)
		{
			var hotelDel = _context.hotel.SingleOrDefault(x => x.Id == Id);
            if (hotelDel != null)
            {
                _context.hotel.Remove(hotelDel);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
			return RedirectToAction("Index");
		}

		public IActionResult CreateNewHotels(Hotel hotels) 
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
			var hotel = _context.hotel.ToList();
            return View("Index",hotel);
        }
    }
}
