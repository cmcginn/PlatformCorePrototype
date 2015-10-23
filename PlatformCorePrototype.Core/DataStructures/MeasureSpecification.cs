namespace PlatformCorePrototype.Core.DataStructures
{
    public class MeasureSpecification
    {
        public DataColumnMetadata Column { get; set; }
        public int DisplayOrder { get; set; }
        public AggregateOperationTypes AggregateOperationType { get; set; }
    }
}