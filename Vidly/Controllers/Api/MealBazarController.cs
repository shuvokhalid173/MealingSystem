using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MealBazarController : ApiController
    {
        private ApplicationDbContext _context;

        public MealBazarController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetMealAccountInfo ()
        {

            string email = AccountController.CurrentUserEmail; // get current user mail
            if (email.Equals("nothing"))
            {
                return BadRequest();
            }
            var meal = _context.MealEvents.Single(c => c.Email.Equals(email)); int idd = meal.Id;// get current user meal event id

            List<Member> members = _context.Members.Where(c => c.MealEvent.Id == idd).ToList(); // get the member of that meal event
            //return Ok(members);

            // this list will store member and member's total meal
            List<Member_Meal> member_Meals = new List<Member_Meal> ();

            // this list will store member and member's total bazar
            List<Member_Bazar> member_Bazars = new List<Member_Bazar> ();

            // finding all meals of each member
            foreach (var member in members)
            {
                decimal totalMealOfthisMember = 0; 

                IEnumerable<EverydaysMeal> everydaysMealOfThisMember = _context
                    .EverydaysMeals.Where(c => c.Member.Id == member.Id && c.Date.Month <= DateTime.Today.Month).ToList();

                

                foreach (var item in everydaysMealOfThisMember)
                {
                    totalMealOfthisMember += item.Breakfast;
                    totalMealOfthisMember += item.Launch;
                    totalMealOfthisMember += item.Dinner;
                }

                Member_Meal member_Meal = new Member_Meal
                {
                    Member = member,
                    TotalMeal = totalMealOfthisMember
                };

                member_Meals.Add(member_Meal);
            }

            // finding all bazars of each member
            foreach (var member in members)
            {
                decimal totalBazarOfthisMember = 0;

                IEnumerable<EverydaysBazar> everydaysBazarOfThisMember = _context
                    .EverydaysBazars.Where(c => c.Member.Id == member.Id && c.Date.Month == DateTime.Today.Month).ToList();

                foreach (var item in everydaysBazarOfThisMember)
                {
                    totalBazarOfthisMember += item.Bazar;
                }

                Member_Bazar member_Meal = new Member_Bazar
                {
                    Member = member,
                    TotalBazar = totalBazarOfthisMember
                };

                member_Bazars.Add(member_Meal);
            }

            MealAccountInfo mealAccountInfo = new MealAccountInfo
            {
                Member_Bazars = member_Bazars , 
                Member_Meals = member_Meals
            };
            return Ok(mealAccountInfo);
        }

        [HttpPost]
        public IHttpActionResult PostResult (MealBazarViewModel mealBazar)
        {
            EverydaysMeal everydaysMeal = new EverydaysMeal
            {
                Breakfast = mealBazar.Breakfast,
                Date = mealBazar.Date,
                Dinner = mealBazar.Dinner,
                Launch = mealBazar.Launch,
                Member = mealBazar.Member
            };

            EverydaysBazar everydaysBazar = new EverydaysBazar
            {
                Bazar = mealBazar.Bazar,
                Date = mealBazar.Date,
                Member = mealBazar.Member
            };

            InsertEverydaysMeal(everydaysMeal);
            InsertEverydaysBazar(everydaysBazar);
            return Ok();
        }

        private void InsertEverydaysBazar(EverydaysBazar everydaysBazar)
        {
            EverydaysBazar newBazar = new EverydaysBazar
            {
                Bazar = everydaysBazar.Bazar,
                Date = everydaysBazar.Date,
                Member = _context.Members.Single(c => c.Id == everydaysBazar.Member.Id)
            };


            var res = _context.EverydaysBazars.SingleOrDefault(c => c.Date.Equals(everydaysBazar.Date) && c.Member.Id == newBazar.Member.Id);
            if (res == null)
            {

                _context.EverydaysBazars.Add(newBazar);

            }
            else
            {
                res.Date = newBazar.Date;
                res.Member = newBazar.Member;
                res.Bazar = newBazar.Bazar;
                //_context.Entry(newMeal).State = EntityState.Modified;
            }


            //            _context.EverydaysBazars.Add(newBazar);
            _context.SaveChanges();
        }

        private void InsertEverydaysMeal(EverydaysMeal everydaysMeal)
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
        }

    }
}
