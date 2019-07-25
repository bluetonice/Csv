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
            navigation = new CsvNavigation();
        }

        public CsvNavigationFactory RelationType(CsvRelationTypeEnum value)
        {
            this.navigation.RelationType = value;
            return this;
        }
        
        public CsvNavigation GetNavigation()
        {
            return this.navigation;
        }
    }
}
