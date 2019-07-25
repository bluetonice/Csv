﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Test
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
