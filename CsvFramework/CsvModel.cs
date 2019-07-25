using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    internal class CsvModel<T> where T :class
    {

        public CsvColumnBuilder<T> Builder { get; set; }

        public string[]  Lines { get; set; }
        public char Seperator { get; set; }

        public bool SkipHeader { get; set; }

        


    }
}
