using CsvFramework;
using CvsParser.Models;
using System;

namespace CvsParser
{
    class Program
    {
        static void Main(string[] args)
        {

            var projectLines = System.IO.File.ReadAllLines(@"C:\temp\data\Projects.csv");
            var orderLines = System.IO.File.ReadAllLines(@"C:\temp\data\Orders.csv");
            var particiantLines = System.IO.File.ReadAllLines(@"C:\temp\data\Participants.csv");
            var activitiesLines = System.IO.File.ReadAllLines(@"C:\temp\data\Activities.csv");


            CsvFactory.Register<Activity>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.OrderId).Type(typeof(int)).Index(1);
                builder.Add(a => a.TaskType).Type(typeof(int)).Index(2);            
            }, true, ',', activitiesLines);

            CsvFactory.Register<Particiant>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.UserId).Type(typeof(int)).Index(2);
                builder.Add(a => a.Role).Type(typeof(string)).Index(3);
                

            }, true, ',', particiantLines);

            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0);
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Keyword).Type(typeof(string)).Index(2);
                builder.Add(a => a.PendingTask).Type(typeof(string)).Index(3);
                builder.Add(a => a.State).Type(typeof(string)).Index(4);
                builder.AddManyToNavigation(a => a.Activities).Type(typeof(Activity));

            }, true, ',', orderLines);

            CsvFactory.Register<Project>(builder =>
           {
               builder.Add(a => a.Id).Type(typeof(int)).Index(0);
               builder.Add(a => a.Name).Type(typeof(string)).Index(1);
               builder.AddManyToNavigation(a => a.Orders).Type(typeof(Order));
               builder.AddManyToNavigation(a => a.Particiants).Type(typeof(Particiant));

           }, true, ',', projectLines);


            var orders = CsvFactory.Parse<Order>();


        }
    }
}
