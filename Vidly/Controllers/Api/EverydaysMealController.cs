using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class EverydaysMealController : ApiController
    {

        private ApplicationDbContext _context;

        public EverydaysMealController()
        {
            this._context = new ApplicationDbContext();
        }

        
        [HttpGet]
        public IHttpActionResult GetPreviousMeal(DateTime date , int id)
        {
            /**
            string email = AccountController.CurrentUserEmail; // get current user mail
            if (email.Equals("nothing"))
            {
                return BadRequest();
            }
            var meal = _context.MealEvents.Single(c => c.Email.Equals(email)); int idd = meal.Id;// get current user meal event id
           
            List<Member> members = _context.Members.Where(c => c.MealEvent.Id == idd).ToList(); // get the member of that meal event
            
            List<int> memberIds = new List<int>(); // push the members id to a list 
            List<string> memberNames = new List<string>();
            foreach (var item in members)
            {
                memberIds.Add(item.Id);
                memberNames.Add(item.Name);
            }
            
            // find the meal record form Everydays meal table using the member Id; 
            return Ok(_context.EverydaysMeals.Where(m => memberIds.Contains(m.Member.Id)).ToList());
            **/

            EverydaysMeal empty = new EverydaysMeal
            {
                Breakfast = 0,
                Dinner = 0,
                Launch = 0,
                Date = date,
                Member = _context.Members.Single(c => c.Id == id)
            };
            var res = _context.EverydaysMeals.SingleOrDefault(c => c.Date.Equals(date) && c.Member.Id == id);
            if (res == null)
            {
                return Ok(empty);
            }
            return Ok(res);

        }
    
        [HttpGet]
        public IHttpActionResult GetMembers(string id)
        {
            
            string email = AccountController.CurrentUserEmail; // get current user mail
            if (email.Equals("nothing"))
            {
                return BadRequest();
            }
            var meal = _context.MealEvents.Single(c => c.Email.Equals(email)); int idd = meal.Id;// get current user meal event id
           
            List<Member> members = _context.Members.Where(c => c.MealEvent.Id == idd).ToList(); // get the member of that meal event
            return Ok(members);
        }

        [HttpPost]
        public IHttpActionResult PostEverydaysMeal (EverydaysMeal everydaysMeal)
        {
            
            
            EverydaysMeal newMeal = new EverydaysMeal
            {
                Breakfast = everydaysMeal.Breakfast,
                Date = everydaysMeal.Date,
                Dinner = everydaysMeal.Dinner,
                Launch = everydaysMeal.Launch,
                Member = _context.Members.Single(c => c.Id == everydaysMeal.Member.Id)
            };

            
            var res = _context.EverydaysMeals.SingleOrDefault(c => c.Date.Equals(everydaysMeal.Date) && c.Member.Id == newMeal.Member.Id);
            if (res == null)
            {

                _context.EverydaysMeals.Add(newMeal);

            }
            else
            {
                res.Breakfast = newMeal.Breakfast;
                res.Launch = newMeal.Launch;
                res.Date = newMeal.Date;
                res.Dinner = newMeal.Dinner;
                res.Member = newMeal.Member;
                //_context.Entry(newMeal).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
