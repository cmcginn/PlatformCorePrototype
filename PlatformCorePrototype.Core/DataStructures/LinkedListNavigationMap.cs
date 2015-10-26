using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListNavigationMap:ILinkedListNavigationMap
    {
        public string Navigation { get; set; }
        public object Key { get; set; }
    }
}
