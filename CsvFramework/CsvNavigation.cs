using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvNavigation
    {
        public CsvRelationTypeEnum RelationType { get; set; }

        public Type RelatedType { get; set; }

        public string RelationKey { get; set; }
    }
}
