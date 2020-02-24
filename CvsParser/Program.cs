using CsvFramework;
using CsvParser.Models;
using CvsParser.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace CvsParser
{

    public class Subs
    {
        public bool Used { get; set; }

        public int count { get; set; }
        public string adid { get; set; }

        //public DateTime created_date { get; set; }

        public string created_day { get; set; }
    }
    public class SubsParsering : IPasering<Subscription>
    {
        Regex regex = new Regex(@"-ADID(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public void Parsing(Subscription item)
        {
            MatchCollection matches = regex.Matches(item.utm_medium_last);


            if (matches.Count > 0)
            {
                item.adid = matches[0].Groups[1].Value;

            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {



            //WebClient webClient = new WebClient();

            //webClient.DownloadFile(new Uri("https://app.wordapp.com/data.zip"), "csv.zip");

            //System.IO.Directory.Delete(@".\temp", true);

            //ZipFile.ExtractToDirectory(@".\csv.zip", @".\temp");


            string urljawabcom = @"C:\temp\dcp_contractss_2020-02-24-171126.csv";
            string urljawabsale = @"C:\temp\dcp_contractss_2020-02-21-115735.csv";

            //var subscriptions = System.IO.File.ReadAllLines(@"C:\temp\dcp_contractss_2020-02-21-115735.csv");
            InsertSubs(urljawabcom,true);
            InsertSubs(urljawabsale);


            //int i = 0;

            //foreach (var item in subs)
            //{
            //    if(item.adid!=null)
            //    {
            //        i++;
            //    }
            //}

            //var k = "";

            //Run();


            //Console.WriteLine(output);
            //Console.ReadLine();



            //var result = CsvFactory.Parse<LandingPage>();

            //string code = "";

            //code += @"List<LandingPage> LandingPages = new List<LandingPage>();";
            //var properties = typeof(LandingPage).GetProperties();

            //foreach (var item in result)
            //{

            //    code += "LandingPages.Add(new LandingPage {";

            //    foreach (var property in properties)
            //    {
            //        code += property.Name;

            //        code += "=";
            //        if (property.PropertyType.Name == "Int32")
            //        {
            //            code += property.GetValue(item, null);
            //        }
            //        else
            //        {
            //            code += $"\"{property.GetValue(item, null)}\"";

            //        }

            //        if (properties.Last().Name != property.Name)
            //        {

            //            code += ",";
            //        }


            //    }

            //    code += "});";
            //    code += Environment.NewLine;
            //}

            //code += "var LandingPage = this.GetQuery<LandingPage>()";
            //code += @"LandingPage.Database.DropCollection(""LandingPage"")";
            //code += "LandingPage.InsertManyAsync(LandingPages)";





        }

        private static void InsertSubs(string url, bool drop = false)
        {
            var subscriptions = System.IO.File.ReadAllLines(url);

            //var orderLines = System.IO.File.ReadAllLines(@".\temp\data\Orders.csv");
            //var particiantLines = System.IO.File.ReadAllLines(@".\temp\data\Participants.csv");
            //var activitiesLines = System.IO.File.ReadAllLines(@".\temp\data\Activities.csv");


            CsvFactory.Register<Subscription>(builder =>
            {
                builder.Add(a => a.paid).Type(typeof(int)).Index(2).IsKey(true);
                builder.Add(a => a.activation_day).Type(typeof(string)).Index(16);
                builder.Add(a => a.created_day).Type(typeof(string)).Index(12);
                builder.Add(a => a.utm_source_last).Type(typeof(string)).Index(25);
                builder.Add(a => a.utm_medium_last).Type(typeof(string)).Index(27);
            }, true, ',', subscriptions);


            IEnumerable<Subscription> subs = CsvFactory.Parse<Subscription>(new SubsParsering());




            var goruped = subs.GroupBy(x => new { x.created_day, x.adid })
                   .Select(g => new Subs { adid = g.Key.adid, created_day = g.Key.created_day, count = g.Sum(s => s.paid) }).ToList();


            const string connectionString = "mongodb://marketing:Marketing2019!@157.55.183.12:27017";
            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("Marketing");
         
            if (drop)
            {
                database.DropCollection("Subscription");
            }
            
            var collection = database.GetCollection<Subs>("Subscription");
            // insert users and return async Task
            collection.InsertMany(goruped);
        }

        private static void Run()
        {
            WebClient webClient = new WebClient();

            webClient.DownloadFile(new Uri("https://app.wordapp.com/data.zip"), "csv.zip");

            System.IO.Directory.Delete(@".\temp", true);

            ZipFile.ExtractToDirectory(@".\csv.zip", @".\temp");


            var projectLines = System.IO.File.ReadAllLines(@".\temp\data\Projects.csv");
            var orderLines = System.IO.File.ReadAllLines(@".\temp\data\Orders.csv");
            var particiantLines = System.IO.File.ReadAllLines(@".\temp\data\Participants.csv");
            var activitiesLines = System.IO.File.ReadAllLines(@".\temp\data\Activities.csv");


            CsvFactory.Register<Activity>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.OrderId).Type(typeof(int)).Index(1);
                builder.Add(a => a.TaskType).Type(typeof(string)).Index(2);
            }, true, ',', activitiesLines);

            CsvFactory.Register<Particiant>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.UserId).Type(typeof(int)).Index(2);
                builder.Add(a => a.Role).Type(typeof(string)).Index(3);


            }, true, ',', particiantLines);

            CsvFactory.Register<Order>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.ProjectId).Type(typeof(int)).Index(1);
                builder.Add(a => a.Keyword).Type(typeof(string)).Index(2);
                builder.Add(a => a.PendingTask).Type(typeof(string)).Index(3);
                builder.Add(a => a.State).Type(typeof(string)).Index(4);
                builder.AddNavigation(n => n.Activities).RelationKey<Activity, int>(k => k.OrderId);

            }, true, ',', orderLines);

            CsvFactory.Register<Project>(builder =>
            {
                builder.Add(a => a.Id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.Name).Type(typeof(string)).Index(1);
                builder.AddNavigation(n => n.Orders).RelationKey<Order, int>(k => k.ProjectId);
                builder.AddNavigation(n => n.Particiants).RelationKey<Particiant, int>(k => k.ProjectId);

            }, true, ',', projectLines);



            IEnumerable<ResultModel> projects = CsvFactory.Parse<Project>().Select(p => new ResultModel
            {
                Id = p.Id,
                Name = p.Name,
                number_of_orders = p.Orders.Count,
                number_of_pending_types = p.Orders.GroupBy(gdc => gdc.PendingTask).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                number_of_participant_types = p.Particiants.GroupBy(gdc => gdc.Role).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),
                number_of_activity_types = p.Orders.SelectMany(o => o.Activities).GroupBy(gdc => gdc.TaskType).ToDictionary(gdc => gdc.Key, gdc => gdc.Count()),

            });

            string output = JsonConvert.SerializeObject(projects, Formatting.Indented);

            Console.WriteLine(output);
            Console.ReadLine();


            var file = System.IO.File.ReadAllText(@"C:\temp\landing_pages.csv");

            var lines = file.Split('\n');

            CsvFactory.Register<LandingPage>(builder =>
            {
                builder.Add(a => a.id).Type(typeof(int)).Index(0).IsKey(true);
                builder.Add(a => a.project_code).Type(typeof(string)).Index(1);
                builder.Add(a => a.country_code).Type(typeof(string)).Index(2);
                builder.Add(a => a.category_id).Type(typeof(int)).Index(3);
                builder.Add(a => a.project_domain).Type(typeof(string)).Index(4);
                builder.Add(a => a.name).Type(typeof(string)).Index(5);
                builder.Add(a => a.language).Type(typeof(string)).Index(6);
                builder.Add(a => a.url).Type(typeof(string)).Index(7);
            }, true, ',', lines);
        }
    }
}
