using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFramework
{
    public interface IPasering<T>
    {
        void Parsing(T item);
    }
}
