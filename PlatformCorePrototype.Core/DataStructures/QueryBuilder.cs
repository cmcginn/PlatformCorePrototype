using System.Collections.Generic;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Core.Models
{
    public class QueryBuilder : IQueryBuilder
    {
        private List<FilterSpecification> _AvailableFilters;
        private List<MeasureSpecification> _AvailableMeasures;
        private List<SlicerSpecification> _AvailableSlicers;
        private List<FilterSpecification> _SelectedFilters;
        private List<MeasureSpecification> _SelectedMeasures;
        private List<SlicerSpecification> _SelectedSlicers;
        public string ViewId { get; set; }
        public List<FilterSpecification> SelectedFilters { get; set; }

        public List<FilterSpecification> AvailableFilters
        {
            get { return _AvailableFilters ?? (_AvailableFilters = new List<FilterSpecification>()); }
            set { _AvailableFilters = value; }
        }


        public List<SlicerSpecification> SelectedSlicers
        {
            get { return _SelectedSlicers ?? (_SelectedSlicers = new List<SlicerSpecification>()); }
            set { _SelectedSlicers = value; }
        }

        public List<MeasureSpecification> SelectedMeasures
        {
            get { return _SelectedMeasures ?? (_SelectedMeasures = new List<MeasureSpecification>()); }
            set { _SelectedMeasures = value; }
        }

        public List<MeasureSpecification> AvailableMeasures
        {
            get { return _AvailableMeasures ?? (_AvailableMeasures = new List<MeasureSpecification>()); }
            set { _AvailableMeasures = value; }
        }

        public List<SlicerSpecification> AvailableSlicers
        {
            get { return _AvailableSlicers ?? (_AvailableSlicers = new List<SlicerSpecification>()); }
            set { _AvailableSlicers = value; }
        }
    }
}