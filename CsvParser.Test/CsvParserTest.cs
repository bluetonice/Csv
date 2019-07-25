using CsvFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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

            CsvFactory.Register2<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);                

            }, true, ',');

            var list = CsvFactory.Parse3<Customer>(lines);

            Assert.IsTrue(5==list.Count);
        }

        [TestMethod]
        public void double_register_test()
        {

            CsvFactory.Register2<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, false, ',');

            CsvFactory.Register2<Customer>(builder =>
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

           

            var list = CsvFactory.Parse3<Customer>(lines);

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

            CsvFactory.Register2<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, true, ',');

            var list = CsvFactory.Parse3<Customer>(lines);

            Assert.IsTrue(4 == list.Count);

            CsvFactory.Register2<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, false, ',');

            list = CsvFactory.Parse3<Customer>(lines);

            Assert.IsTrue(5 == list.Count);
        }

        [TestMethod]
        public void navigation_test()
        {
            string customer = @"1,Ahmet
2,Alper
3,Osman
4,Ayse
5,Melek";

        string order = @"Id,CustomerId,Quantity,Amount
1,1,1,10
2,1,2,25
4,1,1,50
8,1,1,7
3,2,4,32
6,2,2,5
5,5,1,50
7,5,4,4";
                            


            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.CustomerId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Quantity).Type(typeof(int)).Index(2);
                builder.Add(a => a.Amount).Type(typeof(int)).Index(3);               
                

            }, true, ',', order.Split('\n'));

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);
                builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.CustomerId);
                //builder.AddNavigation(n => n.Particiants).RelationKey<Particiant, int>(k => k.ProjectId);               

            }, false, ',', customer.Split("\n"));

            var customers = CsvFactory.Parse<Customer>();

            string output = JsonConvert.SerializeObject(customers, Formatting.Indented);


        }
    }
}
