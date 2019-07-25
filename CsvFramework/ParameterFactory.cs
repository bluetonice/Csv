using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CsvFramework
{
    public class ParameterFactory
    {
        Parameter parameter;

        public ParameterFactory(string Name)
        {
            parameter = new Parameter(Name);
        }

        public ParameterFactory RelationType(RelationTypeEnum value)
        {
            this.parameter.RelationType = value;
            return this;
        }
        public ParameterFactory Type(Type type)
        {
            this.parameter.Type = type;
            return this;
        }

        public ParameterFactory Index(int size)
        {
            this.parameter.Index = size;
            return this;
        }


        public ParameterFactory Value(object value)
        {
            this.parameter.Value = value;
            return this;
        }

        public ParameterFactory ValueToStringArray(IEnumerable<string> value, string seperator = ",")
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


        public Parameter GetParameter()
        {
            return this.parameter;
        }

    }
}
