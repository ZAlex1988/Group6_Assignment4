using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.DataAccess;
using Assignment4.DBModel;
using Assignment4.Models;
using Assignment4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assignment4.Controllers
{
    public class SearchController : Controller
    {
        
        private readonly ILogger<HomeController> log;
        NationalParksData db;
        DataLoader api;
        ChartResponse chartResp;
        public SearchController(ILogger<HomeController> logger, NationalParksData dbContext, DataLoader loader)
        {
            log = logger;
            db = dbContext;
            api = loader;
            //Load Search page chart data from teh API into memory
            chartResp = loader.chartRespData;

        }

        public IActionResult Search()
        {
            log.LogInformation("In search method...");
            SearchViewModel SearchModel = new SearchViewModel();           

            //Instantiate dropdowns
            SearchModel.states = db.ParkState.Select(parkSt => parkSt.StateCode).Distinct().ToList();
            SearchModel.parkCodes = db.Park.Select(park => new ParksAndCodes
            {
                parkCode = park.ParkCode,
                parkName = park.ParkName
            }).ToList();
            log.LogInformation($"Search Model is created: {SearchModel.states.Count} states; {SearchModel.parkCodes.Count} parks are mapped from DB");

            return View("/Views/Home/Search.cshtml", SearchModel);

        }

        [HttpPost]
        public JsonResult FindParks([FromBody] SearchReq info)
        {
            log.LogInformation($"In FindParks method... description: {info.description}; state: {info.state}; parkCode: {info.parkCode}");

            List<SearchResp> response = new List<SearchResp>();

            //find parks matching user search criteria
            List<Park> parks =
                 (from p in db.Park
                    join s in db.ParkState on p.ParkCode equals s.ParkCode
                 where (info.description.Equals("") || (p.ParkName.Contains(info.description) || p.ParkDescription.Contains(info.description))) &&
                       (info.state.Equals("") || s.StateCode.Equals(info.state)) &&
                       (info.parkCode.Equals("") || p.ParkCode.Equals(info.parkCode))
                 select p).Distinct().ToList();

            //map results to output Json mapping other table values to custom reponse
            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    SearchResp resp = new SearchResp();
                    resp.activities =
                        (from actPark in db.ParkActivity
                         join act in db.Activity on actPark.ActivityId equals act.ActivityId
                         where actPark.ParkCode.Equals(park.ParkCode)
                         select act.ActivityName).ToList();
                    resp.fees =
                        (from p in db.Park
                         join f in db.Fee on p.ParkCode equals f.ParkCode
                         where p.ParkCode.Equals(park.ParkCode)
                         select new Entrancefee { cost = f.Cost.ToString(), description = f.Description, title = f.Title })
                         .ToList();
                    resp.url = park.ParkUrl;
                    resp.parkCode = park.ParkCode;
                    resp.fullName = park.ParkName;
                    resp.description = park.ParkDescription;
                    response.Add(resp);
                }
            }
            return new JsonResult(response);

        }

        [HttpPost]
        public JsonResult FindCampgrounds([FromBody] string parkCode)
        {
            log.LogInformation($"In FindCampgrounds method... parkCode: {parkCode}");

            List<Campground> camps = db.Campground.Where(camp => camp.ParkCode.Equals(parkCode)).ToList();
            return new JsonResult(camps);

        }


        [HttpPost]
        public JsonResult GetChartData()
        {
            log.LogInformation("In GetChartData method...");

            return new JsonResult(chartResp);

        }

        




    }
}
