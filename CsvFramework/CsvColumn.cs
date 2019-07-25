using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvColumn

    {
        public CsvColumn(string Name)
        {
            this.Name = Name;        
        }

        
        public string Name { get; set; }
        public Type Type { get; set; }


        public int Index { get; set; }

        public bool IsKey { get; set; }

    }
}
