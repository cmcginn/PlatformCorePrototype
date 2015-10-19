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
       
        public LinkedListPathSpecification SelectedPath { get; set; }
        private List<LinkedListPathSpecification> _AvailablePaths;

        public bool IncludeChildren { get; set; }
        public string SelectedKey { get; set; }

        public List<LinkedListPathSpecification> AvailablePaths
        {
            get { return _AvailablePaths ?? (_AvailablePaths = new List<LinkedListPathSpecification>()); }
            set { _AvailablePaths = value; }
        }
    }
}
