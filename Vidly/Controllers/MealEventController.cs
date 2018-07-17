
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    [Authorize]
    public class MealEventController : Controller
    {
        ApplicationDbContext _context;

        private int counter;

        public MealEventController()
        {
            this._context = new ApplicationDbContext();
            this.counter = _context.MealEvents.Count(c => c.Email.Equals(AccountController.CurrentUserEmail));

        }


        public ActionResult getMail ()
        {
            return Content(AccountController.CurrentUserEmail);
        }
        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult MealBazar ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }
            return View();
        }

        public ActionResult ShowPreviousMeal ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }
            return View();
        }

        public ActionResult EventDetails ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }
            return View(); 
        }
        // GET: MealEvent
        
        public ActionResult EventForm()
        {
            if (counter == 0)
            {
                return View();
            }
            return Content ("You have already created a meal event.");
        }
        
        public ActionResult EditEvent ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm"); 
            }
            var selectedEvent = _context.MealEvents.Single(c => c.Email.Equals(AccountController.CurrentUserEmail));
            List<Member> selectedEventMembers = _context.Members.Where(c => c.MealEvent.Id == selectedEvent.Id).ToList();

            MealEventDto mealEventDto = new MealEventDto
            {
                mealEvent = selectedEvent,
                members = selectedEventMembers
            };
            return View(mealEventDto);
        }

        public ActionResult EntryMeal ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }
            return View(); 
        }

        public ActionResult EntryBazar ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }

            return View();
        }

        public ActionResult ShowPreviousBazar ()
        {
            if (counter == 0)
            {
                return RedirectToAction("EventForm");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }
    }
}