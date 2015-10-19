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

    }
}
