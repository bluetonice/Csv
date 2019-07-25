using CvsParser.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CvsParser.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public List<Activity>  Activities { get; set; }
        public string Keyword { get; set; }

        public string PendingTask { get; set; }

        public string State { get; set; }

    }
}
