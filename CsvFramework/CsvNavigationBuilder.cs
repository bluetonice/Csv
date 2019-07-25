//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace CsvFramework
//{
//    public class CsvNavigationBuilder
//    { 
//        public List<CsvNavigation> Navigations { get; private set; }

//        public CsvNavigationBuilder()
//        {
//            this.Navigations = new List<CsvNavigation>();
//        }
//        public CsvNavigationFactory Add(string name)
//        {

//            CsvNavigationFactory builder = new CsvNavigationFactory(name);
//            this.Navigations.Add(builder.GetNavigation());
//            return builder;
//        }

//        public CsvNavigationFactory Add<T>(string name)
//        {

//            CsvNavigationFactory builder = new CsvNavigationFactory(name);
//            this.Navigations.Add(builder.GetNavigation());
//            return builder;
//        }
//    }
//}
