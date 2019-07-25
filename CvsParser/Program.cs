﻿using CsvFramework;
using CsvParser.Models;
using CvsParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace CvsParser
{
    class Program
    {
        static void Main(string[] args)
        {

            WebClient webClient = new WebClient();
            
            webClient.DownloadFile(new Uri("https://app.wordapp.com/data.zip"), "csv.zip");

            System.IO.Directory.Delete(@".\temp",true);

            ZipFile.ExtractToDirectory(@".\csv.zip", @".\temp");


            var projectLines = System.IO.File.ReadAllLines(@".\temp\data\Projects.csv");
            var orderLines = System.IO.File.ReadAllLines(@".\temp\data\Orders.csv");
            var particiantLines = System.IO.File.ReadAllLines(@".\temp\data\Participants.csv");
            var activitiesLines = System.IO.File.ReadAllLines(@".\temp\data\Activities.csv");


            CsvFactory.Register2<Activity>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.OrderId).Type(typeof(int)).Index(1);
                builder.Add(a => a.TaskType).Type(typeof(string)).Index(2);            
            }, true, ',',activitiesLines);

            CsvFactory.Register2<Particiant>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true); 
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.UserId).Type(typeof(int)).Index(2);
                builder.Add(a => a.Role).Type(typeof(string)).Index(3);
                

            }, true, ',',particiantLines);

            CsvFactory.Register2<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Keyword).Type(typeof(string)).Index(2);
                builder.Add(a => a.PendingTask).Type(typeof(string)).Index(3);
                builder.Add(a => a.State).Type(typeof(string)).Index(4);                
               // builder.AddNavigation(n => n.Activities).RelationKey<Activity, int>(k => k.OrderId);

            }, true, ',',orderLines);

            CsvFactory.Register2<Project>(builder =>
           {
               builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
               builder.Add(a => a.Name).Type(typeof(string)).Index(1);
               builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.ProjectId);
               //builder.AddNavigation(n => n.Particiants).RelationKey<Particiant, int>(k => k.ProjectId);               

           }, true, ',',projectLines);

            var projects2 = CsvFactory.Parse2<Project>();

            IEnumerable<ResultModel> projects = projects2.Select(p => new ResultModel
            {
                Id = p.Id,
                Name = p.Name,
                number_of_orders = p.Orders.Count,
                number_of_pending_types = p.Orders.GroupBy(gdc => gdc.PendingTask).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                //number_of_participant_types = p.Particiants.GroupBy(gdc => gdc.Role).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                //number_of_activity_types = p.Orders.SelectMany(o => o.Activities).GroupBy(gdc => gdc.TaskType).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),

            });

            string output = JsonConvert.SerializeObject(projects, Formatting.Indented);

            Console.WriteLine(output);
            Console.ReadLine();

            //var projects = CsvFactory.Parse2<Project>();
            //var activities = CsvFactory.Parse<Activity>(activitiesLines);
            //var orders = CsvFactory.Parse<Order>(orderLines);
            //var particiants = CsvFactory.Parse<Particiant>(particiantLines);

            //IEnumerable<ResultModel> results = projects.Select(p => new ResultModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    number_of_orders = orders.Count(order => order.ProjectId == p.Id),
            //    number_of_pending_types = orders.Where(order=>order.ProjectId == p.Id).GroupBy(gdc => gdc.PendingTask).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
            //    number_of_participant_types = particiants.Where(pa => pa.ProjectId == p.Id).GroupBy(gdc => gdc.Role).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
            //    number_of_activity_types = activities.Where(o => orders.Where(order => order.ProjectId == p.Id).Select(a=>a.Id).Contains(o.OrderId)).GroupBy(gdc => gdc.TaskType).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),

            //});

            //string output = JsonConvert.SerializeObject(results,Formatting.Indented);

            //Console.WriteLine(output);
            Console.ReadLine();



        }
    }
}
