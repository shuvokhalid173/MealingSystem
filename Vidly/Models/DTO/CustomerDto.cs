using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models.Custom_Annotation;

namespace Vidly.Models.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "please enter a valid Name")]
       /// [UniqeName]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //[Display(Name = "Membership Types")]
        //[Required]
        public byte MembershipTypeId { get; set; }

        //[Display(Name = "Date of Birth")]
        //[Min18AgeIfAMember] // custom validation
        public DateTime? BirthDate { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
    }
}