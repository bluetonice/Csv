using CsvFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvParser.Test
{
    [TestClass]
    public class CsvParserTest
    {


        string customer = @"Id,Name
                            1,Project 1
                            2,Project 2
                            3,Project 3
                            4,Project 4
                            5,Project 5";

        [TestMethod]
        public void csv_parser_test()
        {


            var lines = customer.Split('\n');

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);                

            }, true, ',');

            var list = CsvFactory.Parse<Customer>(lines);

            Assert.IsTrue(5==list.Count);
        }

        [TestMethod]
        public void double_register_test()
        {

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, false, ',');

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, true, ',');


            string customer2 = @"1,Project 1
                            2,Project 2
                            3,Project 3
                            4,Project 4
                            5,Project 5";

            var lines = customer2.Split('\n');

           

            var list = CsvFactory.Parse<Customer>(lines);

            Assert.IsTrue(4 == list.Count);
        }

        [TestMethod]
        public void skip_header_test()
        {
            string customer2 = @"1,Project 1
                            2,Project 2
                            3,Project 3
                            4,Project 4
                            5,Project 5";

            var lines = customer2.Split('\n');

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, true, ',');

            var list = CsvFactory.Parse<Customer>(lines);

            Assert.IsTrue(4 == list.Count);

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, false, ',');

            list = CsvFactory.Parse<Customer>(lines);

            Assert.IsTrue(5 == list.Count);
        }
    }
}
