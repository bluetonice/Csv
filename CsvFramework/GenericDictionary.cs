using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public class CsvModelGenericDictionary
    {
        private Dictionary<string, object> _dict = new Dictionary<string, object>();

        public void Add<T>(string key, CsvModel<T> value) where T : class
        {
            _dict.Add(key, value);
        }

        public CsvModel<T> GetValue<T>(string key) where T : class
        {
            return _dict[key] as CsvModel<T>;
        }
    }
}
