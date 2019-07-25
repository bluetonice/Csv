using System;
using System.Collections.Generic;
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
        public CsvNavigationFactory RelatedType(Type type)
        {
            this.navigation.RelatedType = type;
            return this;
        }

        public CsvNavigationFactory NavigationRelationType(CsvRelationTypeEnum typeEnum)
        {
            this.navigation.RelationType = typeEnum;
            return this;
        }    



        public CsvNavigation GetNavigation()
        {
            return this.navigation;
        }
    }
}
