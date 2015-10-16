using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Core.Models
{
    public class LinkedListQueryBuilder:QueryBuilder
    {
        private List<LinkedListPathSpecification> _SelectedPaths;

        public List<LinkedListPathSpecification> SelectedPaths
        {
            get { return _SelectedPaths ?? (_SelectedPaths = new List<LinkedListPathSpecification>()); }
            set { _SelectedPaths = value; }
        }
    }
}
