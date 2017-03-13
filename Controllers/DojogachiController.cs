using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dojogachi.Controllers
{
    public class DojogachiController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("/Dojogachi")]
        public IActionResult Dojogachi()
        {
            // check win conditions
            if (HttpContext.Session.GetInt32("fullness") > 99
                && HttpContext.Session.GetInt32("happiness") > 99
                && HttpContext.Session.GetInt32("energy") > 99
                )
            {
                return RedirectToAction("win");
            }
            // check loss conditions
            if (HttpContext.Session.GetInt32("fullness") < 1
                || HttpContext.Session.GetInt32("happiness") < 1
                )
            {
                return RedirectToAction("lose");
            }
            // Set Outputs
            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            ViewBag.results = HttpContext.Session.GetString("results");
            ViewBag.gender = HttpContext.Session.GetString("gender");
            ViewBag.pronoun = HttpContext.Session.GetString("pronoun");
            ViewBag.pronoun2 = HttpContext.Session.GetString("pronoun2");
            ViewBag.image = "/images/dojogachi_" + HttpContext.Session.GetString("gender") + ".png";
            // redraw screen
            return View("Dojogachi");
        }
        [HttpGet]
        [Route("/Dojogachi/Feed")]
        public IActionResult Feed()
        {
            int happiness = (int)HttpContext.Session.GetInt32("happiness");
            string results = "";
            string pronoun = HttpContext.Session.GetString("pronoun");
            string pronoun2 = HttpContext.Session.GetString("pronoun2");
            int fullness = (int)HttpContext.Session.GetInt32("fullness");
            int meals = (int)HttpContext.Session.GetInt32("meals");
            int energy = (int)HttpContext.Session.GetInt32("energy");
            if (energy <= 0)
            {
                results = results + "Your dojogachi is exhausted!\nWhy don't you give " + pronoun2 + " a rest?\n";
            }
            if (meals <= 0)
            {
                results = results + "You don't have enough food to feed your Dojogachi!\nGo earn some meals!\n";
            }
            if (results == "")
            {
                meals--;
                Random rand = new Random();
                if (rand.Next(0, 100) < 25)
                {
                    results = HttpContext.Session.GetString("pronoun") + " didn't like the meal.  " + pronoun + " gained no benefits!  \nYou have lost 1 food.";
                }
                else
                {
                    int newfull = rand.Next(5, 10);
                    fullness += newfull;
                    results = HttpContext.Session.GetString("pronoun") + " enjoyed the meal.  " + pronoun + " gained " + newfull.ToString() + " fullness!";
                }
            }
            HttpContext.Session.SetInt32("meals", meals);
            HttpContext.Session.SetInt32("happiness", happiness);
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("fullness", fullness);
            HttpContext.Session.SetString("results", results);
            return RedirectToAction("Dojogachi");
        }
        [HttpGet]
        [Route("/Dojogachi/Play")]
        public IActionResult Play()
        {
            string results = "";
            string pronoun = HttpContext.Session.GetString("pronoun");
            string pronoun2 = HttpContext.Session.GetString("pronoun2");
            int happiness = (int)HttpContext.Session.GetInt32("happiness");
            int energy = (int)HttpContext.Session.GetInt32("energy");
            if (energy <= 0)
            {
                results = "Your dojogachi is exhausted!\nWhy don't you give " + pronoun2 + " a rest?";
            }
            if (results == "")
            {
                Random rand = new Random();
                energy = energy - 5;
                if (rand.Next(0, 100) < 25)
                {
                    results = HttpContext.Session.GetString("pronoun") + " didn't have fun.  " + pronoun + " gained no benefits, and lost 5 energy";
                }
                else
                {
                    int newhappy = rand.Next(5, 10);
                    happiness += newhappy;
                    results = HttpContext.Session.GetString("pronoun") + " had fun playing.  " + pronoun + " gained " + newhappy.ToString() + " happiness!";
                }
            }
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("happiness", happiness);
            HttpContext.Session.SetString("results", results);
            return RedirectToAction("Dojogachi");
        }
        [HttpGet]
        [Route("/Dojogachi/Work")]
        public IActionResult Work()
        {
            string results = "";
            string pronoun = HttpContext.Session.GetString("pronoun");
            string pronoun2 = HttpContext.Session.GetString("pronoun2");
            int happiness = (int)HttpContext.Session.GetInt32("happiness");
            int meals = (int)HttpContext.Session.GetInt32("meals");
            int energy = (int)HttpContext.Session.GetInt32("energy");
            if (energy <= 0)
            {
                results = "Your dojogachi is exhausted!\nWhy don't you give " + pronoun2 + " a rest?";
            }
            if (results == "")
            {
                Random rand = new Random();
                energy = energy - 10;
                happiness = happiness - 5;
                int newmeals = rand.Next(1, 3);
                meals += newmeals;
                results = HttpContext.Session.GetString("pronoun") + " worked hard.  " + pronoun + " earned " + newmeals.ToString() + " meals!, but lost 10 energy, and 5 happiness.";
            }
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("meals", meals);
            HttpContext.Session.SetInt32("happiness", happiness);
            HttpContext.Session.SetString("results", results);
            return RedirectToAction("Dojogachi");
        }

        [HttpGet]
        [Route("/Dojogachi/Sleep")]
        public IActionResult Sleep()
        {
            string results = "";
            string pronoun = HttpContext.Session.GetString("pronoun");
            string pronoun2 = HttpContext.Session.GetString("pronoun2");
            int fullness = (int)HttpContext.Session.GetInt32("fullness");
            int happiness = (int)HttpContext.Session.GetInt32("happiness");
            int energy = (int)HttpContext.Session.GetInt32("energy");
            results = "Your Dojogachi slept for 8 hours.  " + pronoun + " recovered 15 energy!\n" + pronoun + " lost 10 fullness and gained 5 happiness.";
            energy += 15;
            fullness -= 10;
            happiness += 5;
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("happiness", happiness);
            HttpContext.Session.SetInt32("fullness", fullness);
            HttpContext.Session.SetString("results", results);
            return RedirectToAction("Dojogachi");
        }
        [HttpGet]
        [Route("Win")]
        public IActionResult Win()
        {
            return View("Win");
        }
        [HttpGet]
        [Route("Lose")]
        public IActionResult Lose()
        {
            string reason = "";
            string results = "";
            string pronoun = HttpContext.Session.GetString("pronoun");
            string pronoun2 = HttpContext.Session.GetString("pronoun2");
            int happiness = (int)HttpContext.Session.GetInt32("happiness");
            if (happiness < 1)
            {
                results = "Your Dojogachi is too sad, " + pronoun.ToLower() + " has run away!";
                reason = "run";
            }
            else
            {
                results = "Your Dojogachi has starved to death, " + pronoun.ToLower() + " died.";
                reason = "died";
            }
            ViewBag.results = results;
            ViewBag.reason = reason;

            return View("Lose");
        }
    }
}
