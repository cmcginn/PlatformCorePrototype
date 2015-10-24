using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Web.Models
{
    public class LinkedListViewDefinitionModel
    {
        public string MapCollectionName { get; set; }
        public ILinkedListQueryBuilder QueryBuilder { get; set; }
    }
}