using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Vidly.Controllers.Api
{
    public class MealEventController : ApiController
    {

        private ApplicationDbContext _context;

        public MealEventController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<MealEvent> GetMealEvents()
        {
            return _context.MealEvents.ToList();
        }
        
        [HttpGet]
        public IHttpActionResult GetMealEvent (string date , int id)
        {
            var selectedEvent = _context.MealEvents.Single(c => c.Email.Equals(AccountController.CurrentUserEmail));
            //List<int> ids = new List<int>();
            //ids.Add(selectedEvent.Id);
            //List<Member> members = _context.Members.Where(c => ids.Contains(c.MealEvent.Id)).ToList();
            List<Member> selectedEventMembers = _context.Members.Where(c => c.MealEvent.Id == selectedEvent.Id).ToList();
            
            MealEventDto mealEventDto = new MealEventDto
            {
                mealEvent = selectedEvent,
                members = selectedEventMembers
            };
            return Ok(mealEventDto);
        }
         
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {

            string email = AccountController.CurrentUserEmail; // get current user mail
            int idd = _context.MealEvents.Single(c => c.Email.Equals(email)).Id; // get current user meal event id
            List<Member> members =_context.Members.Where(c => c.MealEvent.Id == idd).ToList(); // get the member of that meal event
            List<int> memberIds = new List<int> (); // push the members id to a list 
            foreach (var item in members)
            {
                memberIds.Add(item.Id);
            }
            // find the meal record form Everydays meal table using the member Id; 
            return Ok(_context.EverydaysMeals.Where(m => memberIds.Contains(m.Member.Id)).ToList());
            
            
        }
    
        [HttpPost]
        public IHttpActionResult AddEvent (MealEventWithMembersViewModel NewMealEvent)
        {
            MealEvent newEvent = new MealEvent
            {
                Email = AccountController.CurrentUserEmail ,
                EventName = NewMealEvent.eventName,
                NumberOfMember = NewMealEvent.eventMemberNumber
            };

            //List <string> MemberList = NewMealEvent.Members;

            foreach (var item in NewMealEvent.memberList)
            {
                Member member = new Member
                {
                    MealEvent = newEvent,
                    Name = item.Name
                };
                _context.Members.Add(member);
            }
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IHttpActionResult PutEvent(MealEventWithMembersViewModel NewMealEvent)
        {
            

            var mealEvent = _context.MealEvents.Single(c => c.Email.Equals(AccountController.CurrentUserEmail));
            mealEvent.EventName = NewMealEvent.eventName;
            mealEvent.NumberOfMember = NewMealEvent.eventMemberNumber;

            //List <string> MemberList = NewMealEvent.Members;

            foreach (var item in NewMealEvent.memberList)
            {
                Member member = new Member
                {
                    MealEvent = mealEvent,
                    Name = item.Name
                };
                if (item.Id == 0)
                _context.Members.Add(member);
                else
                {
                    var smember = _context.Members.Single(c => c.Id == item.Id);
                    //smember.MealEvent = mealEvent;
                    if (member.Name.Equals("*"))
                    {
                        _context.Members.Remove(smember);
                    }
                    else 
                    smember.Name = member.Name;
                }
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
