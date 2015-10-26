using System.Collections.Generic;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class DataColumnMetadata
    {
        private List<DataColumnMetadata> _Columns;
        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public List<DataColumnMetadata> Columns
        {
            get { return _Columns ?? (_Columns = new List<DataColumnMetadata>()); }
            set { _Columns = value; }
        }
    }
}