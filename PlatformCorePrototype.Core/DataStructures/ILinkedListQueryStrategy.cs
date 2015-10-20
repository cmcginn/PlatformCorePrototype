using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface ILinkedListQueryStrategy<T,V>:IQueryStrategy<T>
    {
        bool IncludeChildren { get; set; }
        //LinkedListMap<V> LinkedListMap { get; set; }
        List<string> Path { get; set; }
        V Key { get; set; }
        //LinkedListSettings LinkedListSettings { get; set; }
    }
}
