using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{
    public class CsvColumnBuilder
    {
        public List<CsvColumn> Parameters { get; private set; }

        public CsvColumnBuilder()
        {
            this.Parameters = new List<CsvColumn>();
        }
        public CsvColumnFactory Add(string name)
        {

            CsvColumnFactory parameterBuilder = new CsvColumnFactory(name);
            //parameterBuilder.Direction(ParamDirection.Input);
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public CsvColumnFactory Add<T>(string name)
        {

            CsvColumnFactory parameterBuilder = new CsvColumnFactory(name);
            //parameterBuilder.Direction(ParamDirection.Input);
            parameterBuilder.Type(typeof(T));
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }



        public T GetParameterValue<T>(int index)
        {
            var parameter = this.Parameters.Where(p => p.Index == index).SingleOrDefault();

            if (parameter == null) throw new System.Exception(String.Format("{0} parameter not found.", parameter.Name));

            return ConvertHelper.ConvertValue<T>(parameter.Value);
        }

    }
}
