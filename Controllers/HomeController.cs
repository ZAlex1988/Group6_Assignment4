using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4.Models;
using Assignment4.Services;
using Assignment4.DataAccess;
using Assignment4.DBModel;

namespace Assignment4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> log;
        DataLoader Loader;
        NationalParksData db;

        public HomeController(ILogger<HomeController> logger, DataLoader DataLoader, NationalParksData dbContext)
        {
            Loader = DataLoader;
            log = logger;
            db = dbContext;
        }

        public IActionResult Index()
        {
            if (Loader != null && db.Park.Count() == 0) {
                Loader.createAllNationalParksDB();
            } else
            {
                log.LogInformation("Data loader is NULL!");
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


        [HttpPost]
        public JsonResult GetImageUrls([FromBody] string [] parkCodes)
        {
            log.LogInformation($"GetImageUrls({parkCodes.Length})");
            List<ImageUrlsResp> response = new List<ImageUrlsResp>();
            parkCodes.ToList().ForEach(code =>
            {
                ImageUrlsResp resp = new ImageUrlsResp();
                Park park = db.Park.Where(p => p.ParkCode.Equals(code)).FirstOrDefault();
                resp.fullName = park.ParkName;
                resp.images = db.ParkImages.Where(img => img.ParkCode.Equals(code)).ToList();
                response.Add(resp);
            });

            return new JsonResult(response);
        }
    }
}
