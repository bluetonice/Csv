using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class Parameter

    {
        public Parameter(string Name)
        {
            this.Name = Name;
            this.RelationType = RelationTypeEnum.None;
        }

        public RelationTypeEnum RelationType { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }


        public int Index { get; set; }

        public object Value { get; set; }

    }
}
