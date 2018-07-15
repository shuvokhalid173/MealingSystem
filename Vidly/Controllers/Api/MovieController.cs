using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Models.DTO;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MovieController : ApiController
    {

        ApplicationDbContext _context;

        public MovieController()
        {
            this._context = new ApplicationDbContext(); 
        }


        // GET : /api/movie
        public IHttpActionResult GetMovies (string query = null)
        {
            //return Ok(_context.Movies.Include(c => c.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>)); 
            var SelectedMovie = _context.Movies.Include(c => c.Genre);

            if (!string.IsNullOrWhiteSpace(query))
            {
                SelectedMovie = SelectedMovie.Where(m => m.MovieName.Contains(query));
            }

            return Ok(SelectedMovie.ToList().Select(Mapper.Map<Movie , MovieDto>));
        }

        //Get : /api/movie/id
        public IHttpActionResult GetMovie (int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie , MovieDto>(movie));
        }

        //POST: /api/movie
        [HttpPost]
        [Authorize(Roles ="CanManageMovies")]
        public IHttpActionResult CreateMovie (MovieDto movieDto)
        {
            int II; 
            if (ModelState.IsValid)
            {
                var movie = Mapper.Map<MovieDto, Movie>(movieDto); 
                _context.Movies.Add(movie);
                _context.SaveChanges();
                movie.Id = movieDto.Id;
                II = movie.Id;
            }
            else
            {
                 return BadRequest(); 
            }
            return Created(new Uri(Request.RequestUri + "/" + II), movieDto);
        }

        //PUT: /api/movie/id
        [HttpPut]
        [Authorize(Roles = "CanManageMovies")]
        public IHttpActionResult UpdateMovie (int id , MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var selectedMovie = _context.Movies.SingleOrDefault(c => c.Id == id); 
            if (selectedMovie == null)
            {
                return NotFound(); 
            }
            var movie = Mapper.Map<MovieDto, Movie>(movieDto); 
            selectedMovie.Id = id;
            selectedMovie.GenreId = movie.GenreId;
            selectedMovie.AddedDate = movie.AddedDate;
            selectedMovie.MovieName = movie.MovieName;
            selectedMovie.NumberInStock = movie.NumberInStock;
            selectedMovie.ReleaseDate = movie.ReleaseDate;
            _context.SaveChanges();
            return Ok();
        }

        //DELETE: /api/movie/id
        [HttpDelete]
        [Authorize(Roles = "CanManageMovies")]
        public IHttpActionResult DeleteMovie (int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id); 
            if (movie == null)
            {
                return NotFound(); 
            }
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
