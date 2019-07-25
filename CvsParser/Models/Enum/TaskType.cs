using System;
using System.Collections.Generic;
using System.Text;

namespace CvsParser.Models.Enum
{
    public enum TaskType
    {
        WriterTask = 1,
        WebSearchTask = 2,
        EditorTask=4,
        ManagerTask=8,
        PublisherTask=16
    }
}
