using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class MeasureSpecification
    {
        public DataColumnMetadata Column { get; set; }
        public int DisplayOrder { get; set; }
        public AggregateOperationTypes AggregateOperationType { get; set; }
    }
}
