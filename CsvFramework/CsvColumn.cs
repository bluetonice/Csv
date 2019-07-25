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
            this.RelationType = CsvRelationTypeEnum.None;
        }

        public CsvRelationTypeEnum RelationType { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }


        public int Index { get; set; }

        public object Value { get; set; }

    }
}
