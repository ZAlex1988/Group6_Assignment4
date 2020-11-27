using Assignment4.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class ImageUrlsResp
    {
        public string fullName { get; set; }
        public List<ParkImages> images { get; set; }
    }

}
