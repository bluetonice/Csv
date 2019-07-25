using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{
    public class ParameterBuilder
    {
        public List<Parameter> Parameters { get; private set; }

        public ParameterBuilder()
        {
            this.Parameters = new List<Parameter>();
        }
        public ParameterFactory Add(string name)
        {

            ParameterFactory parameterBuilder = new ParameterFactory(name);
            //parameterBuilder.Direction(ParamDirection.Input);
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public ParameterFactory Add<T>(string name)
        {

            ParameterFactory parameterBuilder = new ParameterFactory(name);
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
