using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{
    public class CsvColumnBuilder
    {
        public List<CsvColumn> Columns { get; private set; }

        public CsvColumnBuilder()
        {
            this.Columns = new List<CsvColumn>();
        }
        public CsvColumnFactory Add(string name)
        {

            CsvColumnFactory builder = new CsvColumnFactory(name);
            this.Columns.Add(builder.GetColumn());
            return builder;
        }

        public CsvColumnFactory Add<T>(string name)
        {
            CsvColumnFactory builder = new CsvColumnFactory(name);
            builder.Type(typeof(T));
            this.Columns.Add(builder.GetColumn());
            return builder;
        }        

    }
}
