using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dojogachi.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]

        public IActionResult Index()
        {
            // reinitize block
            HttpContext.Session.Clear();
            Random rand = new Random();
            if (HttpContext.Session.GetString("pronoun") == null)
            {
                if (rand.Next(1, 10) < 6)
                {
                    HttpContext.Session.SetString("gender", "Male");
                    HttpContext.Session.SetString("pronoun", "He");
                    HttpContext.Session.SetString("pronoun2", "Him");
                }
                else
                {
                    HttpContext.Session.SetString("gender", "Female");
                    HttpContext.Session.SetString("pronoun", "She");
                    HttpContext.Session.SetString("pronoun2", "Her");
                }
            }
            String gender = HttpContext.Session.GetString("gender");
            String pronoun = HttpContext.Session.GetString("pronoun");
            String pronoun2 = HttpContext.Session.GetString("pronoun2");
            String results = "You have a new Dojogachi. \n" + pronoun + " needs you to take good care of " + (pronoun2).ToLower() + ".\nGood luck with keeping " + (pronoun2).ToLower() + " alive.";
            HttpContext.Session.SetInt32("fullness", 20);
            HttpContext.Session.SetInt32("happiness", 20);
            HttpContext.Session.SetInt32("meals", 3);
            HttpContext.Session.SetInt32("energy", 50);
            HttpContext.Session.SetString("results", results);
            ViewBag.pronoun2=pronoun2;
            return View();
        }
    }
}
