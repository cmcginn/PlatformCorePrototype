using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class ViewDefinitionMetadata
    {
        public string Id { get; set; }

        public string MetadataCollectionId { get; set; }

        private List<FilterSpecification> _Filters;

        public List<FilterSpecification> Filters
        {
            get { return _Filters ?? (_Filters = new List<FilterSpecification>()); }
            set { _Filters = value; }
        }
    }
}
