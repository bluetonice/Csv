using System;
using System.Collections.Generic;
using System.Text;

namespace CvsParser.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public List<Particiant> Particiants { get; set; }


    }
}
