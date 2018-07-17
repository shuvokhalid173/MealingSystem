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
