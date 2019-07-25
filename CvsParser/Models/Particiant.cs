using System;
using System.Collections.Generic;
using System.Text;

namespace CvsParser.Models
{
    public class Particiant
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }
    }
}
