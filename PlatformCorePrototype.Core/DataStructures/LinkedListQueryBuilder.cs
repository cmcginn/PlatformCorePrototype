using System.Collections.Generic;
using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListQueryBuilder : QueryBuilder, ILinkedListQueryBuilder
    {

        public LinkedListPathSpecification SelectedPath { get; set; }

        public bool ExcludeChildren { get; set; }
        public object SelectedKey { get; set; }
        public int SelectedLevel { get; set; }
        private List<ILinkedListMap> _LinkedListMaps;


        public List<ILinkedListMap> LinkedListMaps
        {
            get { return _LinkedListMaps ?? (_LinkedListMaps = new List<ILinkedListMap>()); }
            set
            {
                _LinkedListMaps = value;
            }
        }
    }
}