using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CsvFramework
{
    public class CsvNavigationFactory
    {
        CsvNavigation navigation;

        public CsvNavigationFactory(string Name)
        {
            navigation = new CsvNavigation(Name);
        }

        public CsvNavigationFactory Type(Type type)
        {
            this.navigation.Type = type;
            return this;
        }


        public CsvNavigationFactory RelationKey<N,TProperty>(Expression<Func<N,TProperty>> key)
        {
            MemberExpression keyBody = key.Body as MemberExpression;
            this.navigation.NavigationName = keyBody.Member.Name;
            this.navigation.Type = typeof(N);            
            return this;
        }



        public CsvNavigation GetNavigation()
        {
            return this.navigation;
        }
    }
}
