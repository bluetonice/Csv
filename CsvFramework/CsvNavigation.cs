using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvNavigation
    {
        public string Name { get; set; }

        public string NavigationName { get; set; }

        public Type Type { get; set; }

        public CsvNavigation(string Name)
        {
            this.Name = Name;
        }

        
    }
}
