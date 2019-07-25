using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CsvFramework
{
    public class ParameterBuilder<T> where T : class
    {

        internal List<Parameter> Columns { get; private set; }

        public ParameterBuilder()
        {
            this.Columns = new List<Parameter>();
        }
        public ParameterFactory Add<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            ParameterFactory parameterBuilder = new ParameterFactory(propertyBody.Member.Name);            
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public ParameterFactory AddManyToNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            ParameterFactory parameterBuilder = new ParameterFactory(propertyBody.Member.Name);
            parameterBuilder.RelationType(RelationTypeEnum.ManyToMany);
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public ParameterFactory AddOneToNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            ParameterFactory parameterBuilder = new ParameterFactory(propertyBody.Member.Name);
            parameterBuilder.RelationType(RelationTypeEnum.OneToMany);
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }
    }
}
