using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class DataCollectionMetadata
    {

        public string Id { get; set; }
        public string DataSourceLocation { get; set; }

        List<DataColumnMetadata> _Columns;

        public List<DataColumnMetadata> Columns
        {
            get { return _Columns ?? (_Columns = new List<DataColumnMetadata>()); }
            set { _Columns = value; }
        }
    }
}
