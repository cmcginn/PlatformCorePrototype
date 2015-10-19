using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IViewDefinitionMetadata
    {
        string ViewId { get; set; }
        List<FilterSpecification> Filters { get; set; }
    }
}