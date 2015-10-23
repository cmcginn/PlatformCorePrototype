using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListViewDefinitionMetadata : ViewDefinitionMetadata
    {
        private List<LinkedListPathSpecification> _Paths;

        public List<LinkedListPathSpecification> Paths
        {
            get { return _Paths ?? (_Paths = new List<LinkedListPathSpecification>()); }
            set { _Paths = value; }
        }
    }
}