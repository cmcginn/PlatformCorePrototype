using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Core.Models
{
    public interface IQueryBuilder
    {
        string ViewId { get; set; }
        List<FilterSpecification> AvailableFilters { get; set; }
        List<MeasureSpecification> AvailableMeasures { get; set; }
        List<SlicerSpecification> AvailableSlicers { get; set; }
        List<FilterSpecification> SelectedFilters{get;set;}
        List<SlicerSpecification> SelectedSlicers { get; set; }
        List<MeasureSpecification> SelectedMeasures { get; set; }
    }
}
