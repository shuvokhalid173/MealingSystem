using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers.Test
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            
            return View();
        }

        
        public ActionResult GetName (string name , string country , int? age)
        {
            
            if (string.IsNullOrWhiteSpace(name))
                name = "shuvo";

            if (string.IsNullOrWhiteSpace(country))
                country = "Bangladesh";

            if (!age.HasValue)
                age = 24;

            return Content(String.Format("{0} from {1} is {2}", name, country, age)); 
        }
        
    }
}