using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;


namespace PlatformCorePrototype.Core.DataStructures
{
    public class ViewDefinition:ViewDefinitionMetadata
    {
        public QueryBuilder QueryBuilder { get; set; }
        public DataDefinition DataDefinition { get; set; }
    }
}
