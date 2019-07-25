using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvFramework
{
    public static class CsvFactory
    {
        static CsvModelGenericDictionary csvModels = new CsvModelGenericDictionary();
        

        public static void Register<T>(Action<ParameterBuilder<T>> builderAction, bool skipheader, char seperator, string[] lines) where T : class, new()
        {
            ParameterBuilder<T> builder = new ParameterBuilder<T>();
            builderAction(builder);
            CsvModel<T> csvModel = new CsvModel<T>();
            csvModel.Builder = builder;
            csvModel.Lines = lines;
            csvModel.Seperator = seperator;
            csvModel.SkipHeader = skipheader;
            csvModels.Add(typeof(T).Name, csvModel);
        }

        public static List<T> Parse<T>() where T : class, new()
        {

            CsvModel<T>  csvModel = csvModels.GetValue<T>(typeof(T).Name);



            List <T> list = new List<T>();

            if (csvModel.SkipHeader)
            {
                csvModel.Lines = csvModel.Lines.Skip(1).ToArray();
            }

            foreach (var line in csvModel.Lines)
            {
                var values = line.Split(csvModel.Seperator);

                var item = new T();

                foreach (var column in csvModel.Builder.Columns)
                {
                    switch (column.RelationType)
                    {
                        case RelationTypeEnum.None:
                            item.GetType().GetProperty(column.Name).SetValue(item, Convert.ChangeType(values[column.Index], column.Type));
                            break;
                        case RelationTypeEnum.OneToMany:

                            break;
                        case RelationTypeEnum.ManyToMany:
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