using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IQueryDefinition
    {
        DataSourceSettings DataSource { get; set; }
        List<FilterSpecification> Filters { get; set; }
    }
}