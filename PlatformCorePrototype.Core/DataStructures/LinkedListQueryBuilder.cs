using System.Collections.Generic;
using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListQueryBuilder : QueryBuilder, ILinkedListQueryBuilder
    {
        private List<LinkedListPathSpecification> _AvailablePaths;
        public LinkedListPathSpecification SelectedPath { get; set; }

        public bool ExcludeChildren { get; set; }
        public object SelectedKey { get; set; }

        public List<LinkedListPathSpecification> AvailablePaths
        {
            get { return _AvailablePaths ?? (_AvailablePaths = new List<LinkedListPathSpecification>()); }
            set { _AvailablePaths = value; }
        }
    }
}