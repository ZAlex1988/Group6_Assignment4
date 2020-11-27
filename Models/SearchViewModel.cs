using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class SearchViewModel
    {
        public List<string> states { get; set; }
        public List<ParksAndCodes> parkCodes { get; set; }

    }
    public class ParksAndCodes
    {
        public string parkCode { get; set; }
        public string parkName { get; set; }
    }
}
