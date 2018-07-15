using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class EverydaysBazar
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal Bazar { get; set; }

        [Required]
        public Member Member { get; set; }
    }
}