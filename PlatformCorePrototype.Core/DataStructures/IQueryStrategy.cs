using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IQueryStrategy
    {
        string CollectionName { get; set; }
        string DataSourceLocation { get; set; }
        List<FilterSpecification> Filters { get; set; }
    }
}
