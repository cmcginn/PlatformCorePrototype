using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class ViewDefinitionMetadata : IViewDefinitionMetadata
    {
        private List<FilterSpecification> _Filters;
        private List<MeasureSpecification> _Measures;
        private List<SlicerSpecification> _Slicers;
        public string ViewId { get; set; }

        public List<FilterSpecification> Filters
        {
            get { return _Filters ?? (_Filters = new List<FilterSpecification>()); }
            set { _Filters = value; }
        }

        public List<SlicerSpecification> Slicers
        {
            get { return _Slicers ?? (_Slicers = new List<SlicerSpecification>()); }
            set { _Slicers = value; }
        }

        public List<MeasureSpecification> Measures
        {
            get { return _Measures ?? (_Measures = new List<MeasureSpecification>()); }
            set { _Measures = value; }
        }
    }
}