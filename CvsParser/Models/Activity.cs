using CvsParser.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CvsParser.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public string TaskType { get; set; }
    }
}
