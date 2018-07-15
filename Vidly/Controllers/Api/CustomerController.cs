using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Models.DTO;

namespace Vidly.Controllers.Api
{

    /**
     * Always send customer dto to client
     * Don't send  customer to client
     * Always take customer dto from client
     * client should have no idea about Customer domain object
    **/

    public class CustomerController : ApiController
    {
        ApplicationDbContext _context;

        public CustomerController()
        {
            this._context = new ApplicationDbContext();
        }

        
        //Get:  /api/customers
        [HttpGet]
        //[Route("api/customers")]
        public IHttpActionResult GetCustomers (string query = null) // make the query string optional 
        {
            var SelectedCustomer = _context.Customers
                .Include(c => c.MembershipType);
         
            if (!string.IsNullOrWhiteSpace(query))
            {
                SelectedCustomer = SelectedCustomer.Where(m => m.Name.Contains(query));
            }
            
            return Ok(SelectedCustomer.ToList()
                .Select(Mapper.Map<Customer , CustomerDto>));
        }

        //Get: /api/customers/1
        //[Route("api/customer/id")]
        public IHttpActionResult GetCustomer (int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer , CustomerDto>(customer));
        }

        //Post: /api/customers
        [HttpPost]
        //[Route("api/customers")]
        public IHttpActionResult CreateCustomer (CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return  Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); ;
        }

        //PUT: /api/customers/1
        [HttpPut]
        //[Route("api/customer/id")]
        public IHttpActionResult UpdateCustomer (int id , CustomerDto customerDto)
        {
            
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (CustomerInDb == null)
            {
                return NotFound(); 
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            CustomerInDb.Id = id;
            CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            CustomerInDb.MembershipTypeId = customer.MembershipTypeId;
            CustomerInDb.Name = customer.Name;
            CustomerInDb.BirthDate = customer.BirthDate;

            _context.SaveChanges();
            return Ok();
        }

        // Delete: /api/customers/1
        [HttpDelete]
        //[Route("api/customer/id")]
        public void DeleteCustomer (int id)
        {
            var Customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //if (Customer == null)
            //{
              //  return NotFound();
            //}
            _context.Customers.Remove(Customer);
            _context.SaveChanges();
            //return Ok();
        }
    }
}
