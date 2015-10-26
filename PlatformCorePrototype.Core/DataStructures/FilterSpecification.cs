using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class FilterSpecification
    {
        private List<FilterSpecification> _Dependencies;

        private List<FilterValue> _FilterValues;
        public DataColumnMetadata Column { get; set; }

        public List<FilterSpecification> Dependencies
        {
            get { return _Dependencies ?? (_Dependencies = new List<FilterSpecification>()); }
            set { _Dependencies = value; }
        }

        public List<FilterValue> FilterValues
        {
            get { return _FilterValues ?? (_FilterValues = new List<FilterValue>()); }
            set { _FilterValues = value; }
        }

        public int DisplayOrder { get; set; }
        public FilterTypes FilterType { get; set; }

        public string SelectionMode { get; set; }
    }
}