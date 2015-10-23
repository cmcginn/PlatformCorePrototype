using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class DataCollectionMetadata : IDataCollectionMetadata
    {
        private List<DataColumnMetadata> _Columns;
        private List<IViewDefinitionMetadata> _Views;
        public string Id { get; set; }
        public string DataSourceLocation { get; set; }

        public string DataSourceName { get; set; }

        public List<DataColumnMetadata> Columns
        {
            get { return _Columns ?? (_Columns = new List<DataColumnMetadata>()); }
            set { _Columns = value; }
        }

        public DataStorageStructureTypes DataStorageType { get; set; }

        public List<IViewDefinitionMetadata> Views
        {
            get { return _Views ?? (_Views = new List<IViewDefinitionMetadata>()); }
            set { _Views = value; }
        }
    }
}