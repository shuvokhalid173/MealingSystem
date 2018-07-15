using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext _context;

        public MovieController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        [AllowAnonymous]
        public ActionResult Index()
        {
            // Not need to do this 
            // because we are getting data from our API 
            //var MovieList = _context.Movies.Include(m => m.Genre).ToList();

            if (User.IsInRole("CanManageMovies"))
                return View();

            return View("ReadOnlyList");
        }

        [Authorize(Roles ="CanManageMovies")]
        public ActionResult MovieForm()
        {
            ViewBag.Header = "Add Movie";
            /**
             * I have  create a new instance of Customer Object
             * Reason : if not then ModelState will be not valid
             * because customer.Id field will be required for valid Model state
             * if don't create object of Customer class  id will not be initialized with 0
             * **/
            Movie movie = new Movie();
            ViewBag.GenreId = new SelectList(_context.Genres, "Id", "MovieType");
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenreId = new SelectList(_context.Genres, "Id", "MovieType", movie.GenreId);
                return View("MovieForm", movie);
            }

            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Today;
                _context.Movies.Add(movie);
            }
            else
                _context.Entry(movie).State = EntityState.Modified;
            
             _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {

            ViewBag.Header = "Edit Movie";
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Movie movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(_context.Genres, "Id", "MovieType", movie.GenreId); 
            return View("MovieForm", movie);
        }
    }
}