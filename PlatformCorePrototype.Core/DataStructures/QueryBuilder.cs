using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Core.Models
{
    public class QueryBuilder:IQueryBuilder
    {
        public string ViewId { get; set; }
        private List<FilterSpecification> _SelectedFilters;
        public List<FilterSpecification> SelectedFilters { get; set; }

        public List<FilterSpecification> AvailableFilters
        {
            get { return _AvailableFilters ?? (_AvailableFilters = new List<FilterSpecification>()); }
            set { _AvailableFilters = value; }
        }



        private List<FilterSpecification> _AvailableFilters;

        private List<SlicerSpecification> _SelectedSlicers;

        public List<SlicerSpecification> SelectedSlicers
        {
            get { return _SelectedSlicers ?? (_SelectedSlicers = new List<SlicerSpecification>()); }
            set
            {
                _SelectedSlicers = value;
            }
        }

        private List<MeasureSpecification> _SelectedMeasures;
        public List<MeasureSpecification> SelectedMeasures
        {
            get { return _SelectedMeasures ?? (_SelectedMeasures = new List<MeasureSpecification>()); }
            set
            {
                _SelectedMeasures = value;
            }
        }

        private List<MeasureSpecification> _AvailableMeasures;
        public List<MeasureSpecification> AvailableMeasures
        {
            get { return _AvailableMeasures ?? (_AvailableMeasures = new List<MeasureSpecification>()); }
            set
            {
                _AvailableMeasures = value;
            }
        }

        private List<SlicerSpecification> _AvailableSlicers;
        public List<SlicerSpecification> AvailableSlicers
        {
            get { return _AvailableSlicers ?? (_AvailableSlicers = new List<SlicerSpecification>()); }
            set
            {
                _AvailableSlicers = value;
            }
        }
    }
}
