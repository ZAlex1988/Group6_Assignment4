using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class SearchResp
    {
        public List<string> activities { get; set; }
        public List<Entrancefee> fees { get; set; }
        public string url { get; set; }
        public string parkCode { get; set; }
        public string fullName { get; set; }
        public string description { get; set; }



    }
}
