using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IDataCollectionMetadata
    {
        string Id { get; set; }
        string DataSourceLocation { get; set; }
        string DataSourceName { get; set; }
        List<DataColumnMetadata> Columns { get; set; }
        DataStorageStructureTypes DataStorageType { get; set; }
        List<IViewDefinitionMetadata> Views { get; set; }
    }
}