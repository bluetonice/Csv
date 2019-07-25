using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvModelGenericDictionary
    {
        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public void Add<T>(string key, CsvModel<T> value) where T : class
        {
            dic.Add(key, value);
        }

        public CsvModel<T> GetValue<T>(string key) where T : class
        {
            return dic[key] as CsvModel<T>;
        }
    }
}
