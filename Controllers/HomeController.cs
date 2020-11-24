using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4.Models;
using Assignment4.Services;

namespace Assignment4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> log;
        DataLoader Loader;

        public HomeController(ILogger<HomeController> logger, DataLoader DataLoader)
        {
            Loader = DataLoader;
            log = logger;
        }

        public IActionResult Index()
        {
            if (Loader != null)
            {
                log.LogInformation("Data loader is not NULL! :)");
                Loader.createAllNationalParksDB();
            } else
            {
                log.LogInformation("Data loader is NULL! :'(");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {

            return View();
        }

        public IActionResult Reservations()
        {

            return View();
        }

        public IActionResult Search()
        {

            return View();
        }
    }
}
