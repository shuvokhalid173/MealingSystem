using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string MovieName { get; set; }
        
        public DateTime? ReleaseDate { get; set; }
        public DateTime? AddedDate { get; set; }

        [Required]
        [Range (1 , 20  , ErrorMessage = "Available movie number should be 1 to 20")]
        public int NumberInStock { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public int GenreId { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        
        public string MovieType { get; set; }
    }
   
}