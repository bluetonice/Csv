using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CsvFramework
{
    public class CsvColumnBuilder<T> where T : class
    {

        internal List<CsvColumn> Columns { get; private set; }

        public CsvColumnBuilder()
        {
            this.Columns = new List<CsvColumn>();
        }
        public CsvColumnFactory Add<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory parameterBuilder = new CsvColumnFactory(propertyBody.Member.Name);            
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public CsvColumnFactory AddOneToManyNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory parameterBuilder = new CsvColumnFactory(propertyBody.Member.Name);
            parameterBuilder.RelationType(CsvRelationTypeEnum.OneToMany);
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public CsvColumnFactory AddOneToOneNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory parameterBuilder = new CsvColumnFactory(propertyBody.Member.Name);
            parameterBuilder.RelationType(CsvRelationTypeEnum.OneToMany);
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public CsvColumnFactory AddManyToManyNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory parameterBuilder = new CsvColumnFactory(propertyBody.Member.Name);
            parameterBuilder.RelationType(CsvRelationTypeEnum.ManyToMany);
            this.Columns.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }
    }
}
