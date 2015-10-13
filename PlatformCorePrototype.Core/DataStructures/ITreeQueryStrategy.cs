using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface ITreeQueryStrategy<T>:IQueryStrategy<T>
    {
        bool IncludeChildren { get; set; }
    }
}
