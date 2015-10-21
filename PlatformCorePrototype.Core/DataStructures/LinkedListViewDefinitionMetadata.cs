using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListViewDefinitionMetadata:ViewDefinitionMetadata
    {
        private List<LinkedListPathSpecification> _Paths;

        public List<LinkedListPathSpecification> Paths
        {
            get { return _Paths ?? (_Paths = new List<LinkedListPathSpecification>()); }
            set { _Paths = value; }
        }
    }
}
