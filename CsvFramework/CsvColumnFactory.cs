using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CsvFramework
{
    public class CsvColumnFactory
    {
        CsvColumn column;

        public CsvColumnFactory(string Name)
        {
            column = new CsvColumn(Name);
        }

        public CsvColumnFactory RelationType(CsvRelationTypeEnum value)
        {
            this.column.RelationType = value;
            return this;
        }
        public CsvColumnFactory Type(Type type)
        {
            this.column.Type = type;
            return this;
        }

        public CsvColumnFactory Index(int size)
        {
            this.column.Index = size;
            return this;
        }


        public CsvColumnFactory IsKey(bool IsKey)
        {
            this.column.IsKey = IsKey;
            return this;
        }

       

        public CsvColumn GetColumn()
        {
            return this.column;
        }

    }
}
