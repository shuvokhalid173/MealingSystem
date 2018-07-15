using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Net;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext _context;

        public CustomerController()
        {
            this._context = new ApplicationDbContext(); 
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var CustomerList = _context.Customers.Include(c => c.MembershipType).ToList();
            
            return View(CustomerList);
        }

        public ActionResult CustomerForm ()
        {
            ViewBag.Header = "Add Customer";
            /**
             * I have  create a new instance of Customer Object
             * Reason : if not then ModelState will be not valid
             * because customer.Id field will be required for valid Model state
             * if don't create object of Customer class  id will not be initialized with 0
             * **/
            Customer customer = new Customer(); 
            ViewBag.MembershipTypeId = new SelectList(_context.MembershipTypes, "Id", "MembershipTypeName");
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save (Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MembershipTypeId = new SelectList(_context.MembershipTypes, "Id", "MembershipTypeName" , customer.MembershipTypeId);
                return View("CustomerForm", customer); 
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer); 
            else
                _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit (int? id)
        {

            ViewBag.Header = "Edit Customer";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembershipTypeId = new SelectList(_context.MembershipTypes, "Id", "MembershipTypeName", customer.MembershipTypeId);
            return View("CustomerForm", customer);
        }
    }
}