using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        [Route("/test")]
        public IActionResult Index()
        {
            List<Animal> animals = new List<Animal>();
            animals.Add(new Animal { AnimalId = 1, AnimalName = "Dog" });
            animals.Add(new Animal { AnimalId = 1, AnimalName = "Cat" });
            return Json(animals);
        }
    }
}