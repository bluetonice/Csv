using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    public class Subscription
    {
        public int paid { get; set; }

        public string activation_day { get; set; }

        public string created_day { get; set; }

        public string utm_source_last { get; set; }

        public string utm_medium_last { get; set; }

        public string adid { get; set; }

        //public string period_type { get; set; }

        //public int is_demo { get; set; }

        //public string country_code { get; set; }
        //public string currency_code { get; set; }

        //public int fully_paid { get; set; }

        //public decimal currency_amount { get; set; }

        //public string payment_gateway { get; set; }

        //public int first_subscription_subject_id { get; set; }
        //public string operator_name { get; set; }
    }
}
