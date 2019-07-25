using CsvFramework;
using CsvParser.Models;
using CvsParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
                builder.Add(a => a.TaskType).Type(typeof(string)).Index(2);            
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
                builder.AddOneToManyNavigation(a => a.Activities).Type(typeof(Activity));

            }, true, ',', orderLines);

            CsvFactory.Register<Project>(builder =>
           {
               builder.Add(a => a.Id).Type(typeof(int)).Index(0);
               builder.Add(a => a.Name).Type(typeof(string)).Index(1);
               builder.AddOneToManyNavigation(a => a.Orders).Type(typeof(Order));
               builder.AddOneToManyNavigation(a => a.Particiants).Type(typeof(Particiant));

           }, true, ',', projectLines);


            var projects2 = CsvFactory.Parse<Project>();

            IEnumerable<ResultModel> projects = CsvFactory.Parse<Project>().Select(p => new ResultModel
            {
                Id = p.Id,
                Name = p.Name,
                number_of_orders = p.Orders.Count(o=>p.Id == p.Id),
                number_of_pending_types = p.Orders.Where(o=>o.Id == p.Id).GroupBy(gdc => gdc.PendingTask).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                number_of_participant_types = p.Particiants.Where(pa => pa.Id == p.Id).GroupBy(gdc => gdc.Role).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                number_of_activity_types = p.Orders.Where(o => o.Id == p.Id).SelectMany(o=>o.Activities).GroupBy(gdc => gdc.TaskType).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),

            });

            string output = JsonConvert.SerializeObject(projects,Formatting.Indented);

            Console.WriteLine(output);
            Console.ReadLine();



        }
    }
}
