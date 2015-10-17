using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListDataCollectionMetadata:DataCollectionMetadata
    {
        public override DataStorageStructureTypes DataStorageType { get { return DataStorageStructureTypes.LinkedList; } }
        public string MapCollectionName { get; set; }
        public DataColumnMetadata KeyColumn { get; set; }
    }
}
