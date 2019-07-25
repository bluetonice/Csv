using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Test
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public string ProductName { get; set; }
    }
}
