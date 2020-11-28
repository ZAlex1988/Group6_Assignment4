using Assignment4.DataAccess;
using Assignment4.DBModel;
using Assignment4.Models;
using Assignment4.Models.CampgroundsData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment4.Services
{
    //Data Loader class provides database loading functionality as well as 
    //NPS API calling functionality
    public class DataLoader
    {
        HttpClient httpClient;
        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "Xui3j5YQKjKLv7a5gqDYxeWdkMft7k9xdtduFSgt";
        private readonly ILogger<DataLoader> log;

        private readonly IServiceScopeFactory scopeFactory;
        public ChartResponse chartRespData { get; set; }


        //Data Loader service is a singleton, so we need a scope factory to use db context in it
        public DataLoader(ILogger<DataLoader> logger, IServiceScopeFactory scope)
        {
            log = logger;
            scopeFactory = scope;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(BASE_URL);

            chartRespData = createChartResponse();
        }
        
        //return a string response from any of the api endpoints by providing a proper uri
        public string getApiResponse(string url)
        {
            string result = "NA";
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).GetAwaiter().GetResult();
                log.LogInformation($"Response for url {url} is {response.StatusCode}; rate limit remaining: {response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault()}");
                
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }
            catch (System.Exception e)
            {
                log.LogError($"Error fetching data for url {url}", e);
            }
            return result;
        }  
        
        //method fetches data from /parks API endpoint in increments of 50 and collects them into a List of datum objects
        //1 Datum object = 1 National Park
        public List<Models.Datum> getParksResponseData()
        {
            int start = 0;
            int limit = 50;
            List<Models.Datum> dataList = new List<Models.Datum>();
            ApiParksResponseData dt = null;
            bool cont = true;
            while (cont)
            {
                try
                {
                    string respData = getApiResponse($"parks?start={start}&limit={limit}");
                    if (!"NA".Equals(respData))
                    {
                        dt = JsonConvert.DeserializeObject<ApiParksResponseData>(respData);
                        if (dt != null && dt.data != null && dt.data.Length != 0)
                        {
                            log.LogInformation($"Total found {dt.total}; start {dt.start}; limit {dt.limit}");
                            dataList.AddRange(dt.data);
                        } else
                        {
                            cont = false;
                            log.LogInformation("Unable to deserialize or reached the end of request");
                        }
                    }
                    else
                    {
                        log.LogInformation("Error fetching data from parks Api");
                        cont = false;
                    }
                    start += limit;

                }
                catch (System.Exception e)
                {
                    log.LogError("Error fetching parks response data", e);
                    cont = false;
                }
            }
            log.LogInformation($"Found {dataList.Count} entries");
            return dataList;
        }

        public ApiCampgroundsData getParkCampgrounds(string parkCode)
        {
            string response = getApiResponse($"campgrounds?parkCode={parkCode}");
            ApiCampgroundsData campData = null;
            if (!"NA".Equals(response))
            {
                campData = JsonConvert.DeserializeObject<ApiCampgroundsData>(response);
            }
            else
            {
                log.LogInformation($"Unable to find campground information about park code {parkCode}");
            }
            return campData;
        }

      
        public void createAllNationalParksDB()
        {
            //using scope factory we get db context instance that is scoped as transient. This make it thread safe to use in a singleton as context is always dynamicaly created
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<NationalParksData>();
                //get all parks 
                List<Models.Datum> allParksData = getParksResponseData();

                //filter out only national parks
                if (allParksData != null && allParksData.Count > 0)
                {
                    allParksData.Where(park => park.designation.Contains("National Park")).ToList()
                        .ForEach(ntPark =>
                        {
                            addParkToDb(db, ntPark);
                            ApiCampgroundsData ntCamps = getParkCampgrounds(ntPark.parkCode);
                            if (ntCamps != null && ntCamps.data != null && ntCamps.data.Length > 0)
                            {
                                addParkCampgroundToDb(db, ntPark.parkCode, ntCamps);
                            }
                        });
                    log.LogInformation("Mapping is complete!");

                }
                else
                {
                    log.LogWarning("Datum list is empty, unable to load national parks data into databasa");
                }
            }
        }

        public void addParkCampgroundToDb(NationalParksData db, string parkCode, ApiCampgroundsData ntCamps)
        {
            foreach (Models.CampgroundsData.Datum ntCamp in ntCamps.data)
            {
                Campground camp = new Campground();
                camp.ParkCode = parkCode;
                camp.CampgroundId = ntCamp.id;
                camp.CampgroundDescription = ntCamp.description;
                camp.CampgroundName = ntCamp.name;
                camp.Sites = int.Parse(ntCamp.campsites.totalSites);
                camp.MaxReservation = camp.Sites;
                camp.reservable = ntCamp.reservationUrl != null && !ntCamp.reservationUrl.Equals("");
                db.Campground.Add(camp);
            }
            db.SaveChanges();
        }

        //Loads parks one by one into the database
        public void addParkToDb(NationalParksData db, Models.Datum ntPark)
        {
            Park park = new Park();
            park.ParkCode = ntPark.parkCode;
            park.ParkName = ntPark.fullName.Contains("Haleakal") ? "Haleakala" : ntPark.fullName;
            park.ParkDescription = ntPark.description;
            park.ParkUrl = ntPark.url;
            db.Park.Add(park);
            db.SaveChanges();
            foreach (Models.Activity ntParkActivity in ntPark.activities)
            {
                string id = db.Activity.Where(act => act.ActivityId.ToUpper().Equals(ntParkActivity.id.ToUpper()))
                    .Select(act => act.ActivityId)
                    .SingleOrDefault();
                if (id == null) {
                    DBModel.Activity activity = new DBModel.Activity();
                    activity.ActivityId = ntParkActivity.id;
                    activity.ActivityName = ntParkActivity.name;
                    db.Activity.Add(activity);
                    db.SaveChanges();

                    ParkActivity parkAct = new ParkActivity();
                    parkAct.ActivityId = activity.ActivityId;
                    parkAct.ParkCode = ntPark.parkCode;
                    db.ParkActivity.Add(parkAct);
                    db.SaveChanges();
                } else
                {
                    ParkActivity parkAct = new ParkActivity();
                    parkAct.ActivityId = ntParkActivity.id;
                    parkAct.ParkCode = ntPark.parkCode;
                    db.ParkActivity.Add(parkAct);
                    db.SaveChanges();
                }
            }

            if (ntPark.entranceFees != null && ntPark.entranceFees.Length > 0)
            {
                foreach (Entrancefee ntParkFee in ntPark.entranceFees) 
                {
                    DBModel.Fee fee = new DBModel.Fee();
                    fee.ParkCode = ntPark.parkCode;
                    fee.Cost = Decimal.Parse(ntParkFee.cost);
                    fee.Description = ntParkFee.description;
                    fee.Title = ntParkFee.title;
                    db.Fee.Add(fee);
                }
            }

            //populate park state lookup table
            foreach (string state in ntPark.states.Split(","))
            {
                ParkState parkState = new ParkState();
                parkState.ParkCode = ntPark.parkCode;
                parkState.StateCode = state;
                db.ParkState.Add(parkState);
            }
            db.SaveChanges();

            foreach (Models.Image img in ntPark.images)
            {
                ParkImages image = new ParkImages();
                image.ParkCode = ntPark.parkCode;
                image.ImageUrl = img.url;
                image.Title = img.title;
                image.Caption = img.caption;
                db.ParkImages.Add(image);
            }

            db.SaveChanges();

        }

        public ChartResponse createChartResponse()
        {
            List<Models.Datum> allParksData = getParksResponseData();
            Dictionary<string, int> stateParkCounts = new Dictionary<string, int>();
            //map parks into dictionary state - numParks (only national parks)
            foreach (Models.Datum park in allParksData)
            {
                if (park.designation.Contains("National Park"))
                {
                    foreach (string state in park.states.Split(","))
                    {
                        if (stateParkCounts.ContainsKey(state))
                        {
                            stateParkCounts[state]++;
                        }
                        else
                        {
                            stateParkCounts.Add(state, 1);
                        }
                    }
                }
            }

            ChartResponse resp = new ChartResponse();
            resp.labels = stateParkCounts.Keys.ToList();
            resp.data = stateParkCounts.Values.ToList();
            resp.colors = generateColors(resp.labels.Count);
            return resp;
        }

        public List<string> generateColors(int numColors)
        {
            Random rnd = new Random();
            List<string> colors = new List<string>();
            for (int i = 0; i < numColors; i++)
            {
                string color = generateRandomColor(rnd);
                log.LogInformation("Color: " + color);
                while (colors.Contains(color))
                {
                    color = generateRandomColor(rnd);

                }
                colors.Add(color);
            }
            return colors;
        }

        public string generateRandomColor(Random rnd)
        {
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return ColorTranslator.ToHtml(randomColor);

        }

    }

    


}
