using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class BazarController : ApiController
    {
        private ApplicationDbContext _context;

        public BazarController()
        {
            this._context = new ApplicationDbContext(); 
        }

        [HttpPost]
        public IHttpActionResult PostBazar (EverydaysBazar everydaysBazar)
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

            return Ok();
        }

        [HttpGet] 
        public IHttpActionResult GetBazar (DateTime date , int id)
        {
            EverydaysBazar empty = new EverydaysBazar
            {
                Bazar = 0,
                Date = date,
                Member = _context.Members.Single(c => c.Id == id)
            };
            var res = _context.EverydaysBazars.SingleOrDefault(c => c.Date.Equals(date) && c.Member.Id == id);
            if (res == null)
            {
                return Ok(empty);
            }
            return Ok(res);

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
