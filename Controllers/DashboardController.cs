using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> SendEmail()
        {
            var Message = new MimeMessage();
            Message.From.Add(new MailboxAddress("Text Message", "lmattractive24@gmail.com"));
            Message.To.Add(MailboxAddress.Parse("ispoilt24@gmail.com"));
            Message.Subject = "Test Email From My project in Asp.net Core MVC";
            Message.Body = new TextPart("Plain")
            {
                Text = "Welcome in my App"   
            };
            using (var client=new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("lmattractive24@gmail.com", "xzybdfqfppamvzes");
                    await client.SendAsync(Message);
                    client.Disconnect(true);
                }
                catch (Exception e){ 
                    return e.Message.ToString();
                
                }   
            }
            return "Ok";
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

        public IActionResult UpdateRooms(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.rooms.Attach(rooms);
                _context.Entry(rooms).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Rooms");
            }
            return View("EditRooms");
        }



        public IActionResult EditRooms(int Id)
        {
            var EditRooms = _context.rooms.SingleOrDefault(x => x.Id == Id);
            ViewBag.hotel = _context.hotel.ToList();
            return View(EditRooms);
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

        public IActionResult UpdateRoomDetails(RoomDetails roomDetails)
        {
            if (ModelState.IsValid)
            {
                _context.roomDetails.Attach(roomDetails);
                _context.Entry(roomDetails).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("RoomDetails");
            }
            return View("EditRoomDetails");
        }



        public IActionResult EditRoomDetails(int Id)
        {
            var RoomDetailsEdit = _context.roomDetails.SingleOrDefault(x => x.Id == Id);
            ViewBag.hotel = _context.hotel.ToList();
            ViewBag.rooms = _context.rooms.ToList();
            return View(RoomDetailsEdit);
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

//xzyb dfqf ppam vzes