using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int number_of_orders { get; set; }

        public Dictionary<string,int> number_of_pending_types { get; set; }
        public Dictionary<string, int> number_of_participant_types { get; set; }
        public Dictionary<string, int> number_of_activity_types { get; set; }


    }

   
}
