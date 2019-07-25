using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CsvFramework
{
    public class CsvColumnBuilder<T> where T : class
    {

        internal List<CsvColumn> Columns { get; private set; }
        internal List<CsvNavigation> Navigations { get; private set; }


        public CsvColumnBuilder()
        {
            this.Columns = new List<CsvColumn>();
            this.Navigations = new List<CsvNavigation>();
        }
        public CsvColumnFactory Add<TProperty>(Expression<Func<T, TProperty>> column)
        {
            MemberExpression propertyBody = column.Body as MemberExpression;
            CsvColumnFactory builder = new CsvColumnFactory(propertyBody.Member.Name);            
            this.Columns.Add(builder.GetColumn());
            return builder;
        }

        public CsvNavigationFactory AddNavigation<N, TProperty>(Expression<Func<N, TProperty>> column)
        {
            MemberExpression propertyBody = column.Body as MemberExpression;
            CsvNavigationFactory builder = new CsvNavigationFactory(propertyBody.Member.Name);
            this.Navigations.Add(builder.GetNavigation());
            return builder;
        }

      
    }
}
