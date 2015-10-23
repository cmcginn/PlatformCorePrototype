using System.Collections.Generic;
using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface ILinkedListQueryBuilder : IQueryBuilder
    {
        bool ExcludeChildren { get; set; }
        List<LinkedListPathSpecification> AvailablePaths { get; set; }
        LinkedListPathSpecification SelectedPath { get; set; }
        object SelectedKey { get; set; }
    }
}