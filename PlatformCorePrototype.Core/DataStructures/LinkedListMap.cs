using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListMap:ILinkedListMap
    {
        public string SlicerColumnName { get; set; }
        private List<ILinkedListNavigationMap> _NavigationMaps;
        public List<ILinkedListNavigationMap> NavigationMaps
        {
            get { return _NavigationMaps ?? (_NavigationMaps = new List<ILinkedListNavigationMap>()); }
            set
            {
                _NavigationMaps = value;
            }
        }
    }
}
