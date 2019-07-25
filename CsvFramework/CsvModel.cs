using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvModel<T> where T :class
    {

        public CsvColumnBuilder<T> Builder { get; set; }
        
        public char Seperator { get; set; }

        public bool SkipHeader { get; set; }

        


    }
}
