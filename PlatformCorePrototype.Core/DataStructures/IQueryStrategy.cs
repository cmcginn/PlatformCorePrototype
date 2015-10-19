using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IQueryStrategy<T>
    {
        DataCollectionMetadata CollectionMetadata { get; set; }
        ViewDefinitionMetadata ViewDefinitionMetadata { get; set; }
        List<FilterSpecification> Filters { get; set; }
        string ViewId { get; set; }
        Task<List<T>> RunQuery();
    }
}
