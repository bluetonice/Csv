using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{

    
    public static class CsvFactory
    {
        static CsvModelGenericDictionary csvModels = new CsvModelGenericDictionary();

        //TODO:away static
        static List<CsvFilterItem> filters = new List<CsvFilterItem>();

        

        public static void Register<T>(Action<CsvColumnBuilder<T>> builderAction, bool skipheader, char seperator, string[] lines) where T : class, new()
        {
            var name = typeof(T).Name;

            if (csvModels.IsExist(name)) csvModels.Remove(name);

            CsvColumnBuilder<T> builder = new CsvColumnBuilder<T>();
            builderAction(builder);
            CsvModel<T> csvModel = new CsvModel<T>();
            csvModel.Lines = lines;
            csvModel.Builder = builder;
            csvModel.Seperator = seperator;
            csvModel.SkipHeader = skipheader;
            csvModels.Add(name, csvModel);
        }




        //TODO:Mesaure Performance and 
        public static List<T> Parse<T>(IPasering<T> pasering =null) where T : class, new()
        {


            CsvModel<T> csvModel = csvModels.GetValue<T>(typeof(T).Name);

            List<T> list = new List<T>();

            if (csvModel.SkipHeader)
            {
                csvModel.Lines = csvModel.Lines.Skip(1).ToArray();
            }

            int index = 0;

            if (filters.Any())
            {
                var currentFilter = filters.SingleOrDefault(f=>f.CsvName == typeof(T).Name);
                if (currentFilter != null)
                {
                    index = csvModel.Builder.Columns.SingleOrDefault(c => c.Name == currentFilter.Name).Index;
                }
            }

            foreach (var line in csvModel.Lines)
            {

                if(String.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(csvModel.Seperator);

                if (filters.Any())
                {
                    var currentFilter = filters.SingleOrDefault(f => f.CsvName == typeof(T).Name);
                    if (currentFilter != null)
                    {
                        if (values[index] != currentFilter.Value) continue;
                    }
                }

                var item = new T();

                foreach (var column in csvModel.Builder.Columns)
                {
                    
                    //var value = values[column.Index].Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                    //if (column.Name == "url") 
                    //{ 
                    //    value = value.Remove(value.Length - 1);
                    //    value = value.Remove(value.Length - 1);
                    //}

                    item.GetType().GetProperty(column.Name).SetValue(item, Convert.ChangeType(values[column.Index], column.Type));
                }


                foreach (var navigation in csvModel.Builder.Navigations)
                {
                    string navigationValue = values[csvModel.Builder.Columns.Where(c => c.IsKey).SingleOrDefault().Index];

                    var currentFilter = filters.SingleOrDefault(f => f.CsvName == navigation.Type.Name);

                    if (currentFilter == null)
                    {
                        filters.Add(new CsvFilterItem { CsvName = navigation.Type.Name, Name = navigation.NavigationName, Value = navigationValue });
                    }
                    else
                    {
                        currentFilter.Value = navigationValue;
                    }

                    var @object = typeof(CsvFactory)
                          .GetMethod("Parse")
                          .MakeGenericMethod(navigation.Type)
                          .Invoke(null, null);

                    item.GetType().GetProperty(navigation.Name).SetValue(item, @object);

                }

                pasering.Parsing(item);

                list.Add(item);
            }

            return list;
        }






    }
}