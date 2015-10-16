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

    }
}
