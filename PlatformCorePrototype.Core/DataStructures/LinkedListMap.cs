using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListMap<T>
    {
        public T Key { get; set; }

        public List<string> Navigation
        {
            get { return _Navigation ?? (_Navigation = new List<string>()); }
            set { _Navigation = value; }
        }

        private List<string> _Navigation;


    }
}
