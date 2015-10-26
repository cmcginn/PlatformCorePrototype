using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IQueryStrategy<T>
    {
        IQueryBuilder QueryBuilder { get; set; }

        Task<List<T>> RunQuery();
    }
}