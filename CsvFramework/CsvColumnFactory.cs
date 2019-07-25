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
        CsvColumn parameter;

        public CsvColumnFactory(string Name)
        {
            parameter = new CsvColumn(Name);
        }

        public CsvColumnFactory RelationType(CsvRelationTypeEnum value)
        {
            this.parameter.RelationType = value;
            return this;
        }
        public CsvColumnFactory Type(Type type)
        {
            this.parameter.Type = type;
            return this;
        }

        public CsvColumnFactory Index(int size)
        {
            this.parameter.Index = size;
            return this;
        }


        public CsvColumnFactory Value(object value)
        {
            this.parameter.Value = value;
            return this;
        }

        public CsvColumnFactory ValueToStringArray(IEnumerable<string> value, string seperator = ",")
        {
            if (value != null && value.Any())
            {
                this.parameter.Value = String.Join(seperator, value.Select(a => String.Format("'{0}'", a)));
            }
            else
            {
                this.parameter.Value = String.Empty;
            }

            return this;
        }


        public CsvColumn GetParameter()
        {
            return this.parameter;
        }

    }
}
