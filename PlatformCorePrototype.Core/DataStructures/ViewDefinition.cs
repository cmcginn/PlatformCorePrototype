using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class ViewDefinition : ViewDefinitionMetadata
    {
        public QueryBuilder QueryBuilder { get; set; }
        public DataDefinition DataDefinition { get; set; }
    }
}