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
            CsvColumnFactory builder = new CsvColumnFactory(propertyBody.Member.Name);            
            this.Columns.Add(builder.GetColumn());
            return builder;
        }

        public CsvColumnFactory AddOneToManyNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory builder = new CsvColumnFactory(propertyBody.Member.Name);
            builder.RelationType(CsvRelationTypeEnum.OneToMany);
            this.Columns.Add(builder.GetColumn());
            return builder;
        }

        public CsvColumnFactory AddOneToOneNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory builder = new CsvColumnFactory(propertyBody.Member.Name);
            builder.RelationType(CsvRelationTypeEnum.OneToMany);
            this.Columns.Add(builder.GetColumn());
            return builder;
        }

        public CsvColumnFactory AddManyToManyNavigation<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;
            CsvColumnFactory builder = new CsvColumnFactory(propertyBody.Member.Name);
            builder.RelationType(CsvRelationTypeEnum.ManyToMany);
            this.Columns.Add(builder.GetColumn());
            return builder;
        }
    }
}
