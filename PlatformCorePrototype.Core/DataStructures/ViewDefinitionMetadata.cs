using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class ViewDefinitionMetadata : IViewDefinitionMetadata
    {
        public string ViewId { get; set; }

        private List<FilterSpecification> _Filters;

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

        private List<SlicerSpecification> _Slicers;

        private List<MeasureSpecification> _Measures;
    }
}
