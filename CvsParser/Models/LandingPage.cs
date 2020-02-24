using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    public class LandingPage
    {
        public int id { get; set; }

        public string project_code { get; set; }

        public string country_code { get; set; }

        public int category_id { get; set; }

        public string project_domain { get; set; }

        public string language { get; set; }

        public string name { get; set; }
        public string url { get; set; }


    }
}
