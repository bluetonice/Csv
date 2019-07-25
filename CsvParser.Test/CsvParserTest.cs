using CsvFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;

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

            }, true, ',',lines);

            var list = CsvFactory.Parse<Customer>();

            Assert.IsTrue(5==list.Count);
        }

        [TestMethod]
        public void double_register_test()
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

            }, false, ',',lines);

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, true, ',',lines);

            var list = CsvFactory.Parse<Customer>();

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

            }, true, ',',lines);

            var list = CsvFactory.Parse<Customer>();

            Assert.IsTrue(4 == list.Count);

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);

            }, false, ',',lines);

            list = CsvFactory.Parse<Customer>();

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

            var customerLines = customer.Split("\n");

        string order = @"Id,CustomerId,Quantity,Amount
1,1,1,10
2,1,2,25
4,1,1,50
8,1,1,7
3,2,4,324
6,2,2,5
5,5,1,50
7,5,4,4";

            var orderLines = order.Split("\n");

            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.CustomerId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Quantity).Type(typeof(int)).Index(2);
                builder.Add(a => a.Amount).Type(typeof(int)).Index(3);               
                

            }, true, ',', orderLines);

            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);
                builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.CustomerId);

            }, false, ',', customerLines);

            var customers = CsvFactory.Parse<Customer>();

            Assert.IsTrue(customers.Count == 5);
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ahmet").Orders.Count == 4,"Ahmet");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Alper").Orders.Count == 2,"Alper");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Osman").Orders.Count==0,"Osman");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ayse").Orders.Count ==0,"Ayse");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Melek").Orders.Count == 2,"Melek");
            


        }


        [TestMethod]//navigation support infinity
        public void navigation_of_navigation_test()
        {
            string customer = @"1,Ahmet
2,Alper
3,Osman
4,Ayse
5,Melek";

            var customerLines = customer.Split("\n");

            string order = @"Id,CustomerId,TotalQuantity,TotalAmount
1,1,1,10
2,1,2,25
3,1,1,50
4,1,1,7
5,2,4,324
6,2,2,5
7,5,1,50
8,5,4,4";

            var orderLines = order.Split("\n");

            string orderItem = @"Id,OrderId,ProductName
1,1,1
2,8,2
3,1,1
4,2,1
5,7,4
6,3,2
7,6,1
8,3,1
9,4,1
10,4,1
11,1,1
12,8,1
13,1,1
14,5,1
15,4,1
16,5,1
17,7,1
18,8,1
19,5,4";

            var orderItemLines = orderItem.Split("\n");






            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);
                builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.CustomerId);

            }, false, ',', customerLines);

            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.CustomerId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Quantity).Type(typeof(int)).Index(2);
                builder.Add(a => a.Amount).Type(typeof(int)).Index(3);
                builder.AddNavigation(n => n.OrderItems).RelationKey<OrderItem, int>(k => k.OrderId);

            }, true, ',', orderLines);


            CsvFactory.Register<OrderItem>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.OrderId).Type(typeof(int)).Index(1);
                builder.Add(a => a.ProductName).Type(typeof(string)).Index(2);


            }, false, ',', orderItemLines);



            var customers = CsvFactory.Parse<Customer>();

            Assert.IsTrue(customers.Count == 5);
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ahmet").Orders.Count == 4, "Ahmet");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Alper").Orders.Count == 2, "Alper");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Osman").Orders.Count == 0, "Osman");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ayse").Orders.Count == 0, "Ayse");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Melek").Orders.Count == 2, "Melek");




            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ahmet").Orders.SelectMany(s => s.OrderItems).Count() == 10, "Ahmet");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Alper").Orders.SelectMany(s => s.OrderItems).Count() == 4, "Alper");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Osman").Orders.SelectMany(s => s.OrderItems).Count() == 0, "Osman");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ayse").Orders.SelectMany(s => s.OrderItems).Count() == 0, "Ayse");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Melek").Orders.SelectMany(s => s.OrderItems).Count() == 5, "Melek");


        }


        [TestMethod]//multiple navigation support infinity
        public void multiple_navigation_test()
        {
            string customer = @"1,Ahmet
2,Alper
3,Osman
4,Ayse
5,Melek";

            var customerLines = customer.Split("\n");

            string order = @"Id,CustomerId,TotalQuantity,TotalAmount
1,1,1,10
2,1,2,25
3,1,1,50
4,1,1,7
5,2,4,324
6,2,2,5
7,5,1,50
8,5,4,4";

            var orderLines = order.Split("\n");

            string adresItem = @"Id,CustomerId,Name
1,1,Ev
2,1,Ev
3,2,Is
4,3,Ev
5,4,Is
6,5,IS
7,1,Is
8,3,Ev
9,3,Ev";

            var adresLines = adresItem.Split("\n");






            CsvFactory.Register<Customer>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);
                builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.CustomerId);
                builder.AddNavigation(n => n.Addresses).RelationKey<Address, int>(k => k.CustomerId);

            }, false, ',', customerLines);

            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.CustomerId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Quantity).Type(typeof(int)).Index(2);
                builder.Add(a => a.Amount).Type(typeof(int)).Index(3);                
                
            }, true, ',', orderLines);


            CsvFactory.Register<Address>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.CustomerId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Name).Type(typeof(string)).Index(2);


            }, false, ',', adresLines);



            var customers = CsvFactory.Parse<Customer>();

            Assert.IsTrue(customers.Count == 5);
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ahmet").Orders.Count == 4, "Ahmet");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Alper").Orders.Count == 2, "Alper");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Osman").Orders.Count == 0, "Osman");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ayse").Orders.Count == 0, "Ayse");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Melek").Orders.Count == 2, "Melek");

            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ahmet").Addresses.Count == 3, "Ahmet");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Alper").Addresses.Count == 1, "Alper");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Osman").Addresses.Count == 3, "Osman");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Ayse").Addresses.Count == 1, "Ayse");
            Assert.IsTrue(customers.SingleOrDefault(c => c.Name == "Melek").Addresses.Count == 1, "Melek");







        }
    }
}
