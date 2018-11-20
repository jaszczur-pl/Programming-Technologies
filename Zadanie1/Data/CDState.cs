using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie1.Data
{
    public class CDState
    {
        public CD cd { get; set; }
        public string title { get; set; }
        public string group { get; set; }
        public DateTimeOffset dateOfPurchase { get; set; }
    }
}
