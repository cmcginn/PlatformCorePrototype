using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IViewDefinitionMetadata
    {
        string Id { get; set; }
        string MetadataCollectionId { get; set; }
        List<FilterSpecification> Filters { get; set; }
    }
}