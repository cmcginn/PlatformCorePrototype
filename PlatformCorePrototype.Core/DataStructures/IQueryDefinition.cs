using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IQueryDefinition<T>
    {
        string CollectionName { get; set; }
        string DataSourceName { get; set; }
        string DataSourceLocation { get; set; }
        List<FilterSpecification> Filters { get; set; }
        Task<List<T>> RunQuery();
    }
}