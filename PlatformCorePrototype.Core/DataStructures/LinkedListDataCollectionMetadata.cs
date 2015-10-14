using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class LinkedListDataCollectionMetadata:DataCollectionMetadata
    {
        public DataCollectionMetadata SourceCollectionMetadata { get; set; }
        public DataCollectionMetadata MapCollectionMetadata { get; set; }
        public string ValueColumnName { get; set; }
        public string NavigationColumnName{ get; set; }


    }
}
