using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{
    public static class CsvFactory
    {
        static CsvModelGenericDictionary csvModels = new CsvModelGenericDictionary();
        

        public static void Register<T>(Action<CsvColumnBuilder<T>> builderAction, bool skipheader, char seperator) where T : class, new()
        {
            var name = typeof(T).Name;

            if (csvModels.IsExist(name)) csvModels.Remove(name);

            CsvColumnBuilder<T> builder = new CsvColumnBuilder<T>();
            builderAction(builder);
            CsvModel<T> csvModel = new CsvModel<T>();
            csvModel.Builder = builder;            
            csvModel.Seperator = seperator;
            csvModel.SkipHeader = skipheader;            
            csvModels.Add(name, csvModel);
        }

        public static List<T> Parse<T>(string[] lines) where T : class, new()
        {


            CsvModel<T>  csvModel = csvModels.GetValue<T>(typeof(T).Name);

            List <T> list = new List<T>();

            if (csvModel.SkipHeader)
            {
                lines = lines.Skip(1).ToArray();
            }

            foreach (var line in lines)
            {
                var values = line.Split(csvModel.Seperator);

                var item = new T();

                foreach (var column in csvModel.Builder.Columns)
                {
                    switch (column.RelationType)
                    {
                        case CsvRelationTypeEnum.None:
                            item.GetType().GetProperty(column.Name).SetValue(item, Convert.ChangeType(values[column.Index], column.Type));
                            break;
                        case CsvRelationTypeEnum.OneToMany:
                            var @object = typeof(CsvFactory)
                                  .GetMethod("Parse")
                                  .MakeGenericMethod(column.Type)
                                  .Invoke(null,null);
                            item.GetType().GetProperty(column.Name).SetValue(item, @object);
                            break;
                        case CsvRelationTypeEnum.ManyToMany:
                            break;
                        default:
                            item.GetType().GetProperty(column.Name).SetValue(item, Convert.ChangeType(values[column.Index], column.Type));
                            break;
                    }

                    
                }

                list.Add(item);
            }

            return list;
        }
    }
}